using System;
using System.Collections.Generic;
using System.Linq;
using Controller.Character.Player.Player;
using Level_Editor.Runtime;
using Model.Interface;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Tools.Level_Editor.Runtime.Component.Trigger;
using UnityEngine.Events;

namespace QFramework.Example
{
	public class TriggerPanelData : UIPanelData
	{
	}
	
	public partial class TriggerPanel : UIPanel, IController
	{
		private Transform player;
		private Camera cam;
		private PlayerConfig config;
		private Dictionary<TriggerController, TriggerNoticeController> triggerUIDict = new Dictionary<TriggerController, TriggerNoticeController>();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TriggerPanelData ?? new TriggerPanelData();
			
			config = Resources.Load<PlayerConfig>("Data/Character/Player/Player Config");
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			player = this.GetModel<IPlayerModel>().components.Get<Transform>();
			cam = Camera.main;
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}

		private void Update()
		{
			ShowTriggers();
		}

		public void ShowTriggers()
		{
			var triggers = TriggerManager.Instance.interactableTriggers.ToList();
			for (var i = 0; i < triggers.Count; i++)
			{
				var controller = triggers[i].Key;
				var info = triggers[i].Value;
				
				TriggerNoticeController noticeController = null;
				if (!triggerUIDict.ContainsKey(controller))
				{
					noticeController = Resources.Load<GameObject>("UI Prefab/Trigger Notice")
						.Instantiate()
						.Parent(transform)
						.Name("Trigger Notice")
						.GetComponent<TriggerNoticeController>();
					
					triggerUIDict.Add(controller, noticeController);
				}
				else
				{
					noticeController = triggerUIDict[controller];
				}

				var point = info.interactPoint;
				var viewportPos = cam.WorldToViewportPoint(point.position);
				var inDistance = Vector3.Distance(player.position, point.position) < config.triggerDetectDistance;

				var isVisible = (viewportPos.x is > 0 and < 1 && 
				                 viewportPos.y is > 0 and < 1 && 
				                 viewportPos.z > 0) && inDistance;

				if (!isVisible || controller.State != TriggerState.Pending)
				{
					noticeController.State = TriggerNoticeState.None;
				}
				else
				{
					var conditionFunc = info.condition;
					
					noticeController.transform.position = cam.WorldToScreenPoint(point.position);
					noticeController.State = conditionFunc() ? TriggerNoticeState.Near :  TriggerNoticeState.Far;
				}
			}
		}

		public IArchitecture GetArchitecture()
		{
			return ZeldaLike.Interface;
		}
	}
}
