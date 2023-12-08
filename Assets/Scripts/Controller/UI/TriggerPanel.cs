using System;
using System.Collections.Generic;
using System.Linq;
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
		private Dictionary<Transform, TriggerNoticeController> triggerUIDict = new Dictionary<Transform, TriggerNoticeController>();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TriggerPanelData ?? new TriggerPanelData();
			// please add init code here
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
			var triggers = TriggerManager.Instance.interactableTriggers.Keys;

			foreach (var trigger in triggers)
			{
				TriggerNoticeController noticeController = null;
				if (!triggerUIDict.ContainsKey(trigger))
				{
					noticeController = Resources.Load<GameObject>("UI Prefab/TriggerNotice")
						.Instantiate()
						.Parent(transform)
						.Name("TriggerNotice")
						.GetComponent<TriggerNoticeController>();
					
					triggerUIDict.Add(trigger, noticeController);
				}
				else
				{
					noticeController = triggerUIDict[trigger];
				}
				
				var viewportPos = cam.WorldToViewportPoint(trigger.position);
				var inDistance = Vector3.Distance(player.position, trigger.position) < 10f;

				var isVisible = (viewportPos.x is > 0 and < 1 && 
				                 viewportPos.y is > 0 and < 1 && 
				                 viewportPos.z > 0) && inDistance;

				if (!isVisible)
				{
					noticeController.State = TriggerNoticeState.None;
				}
				else
				{
					var conditionFunc = TriggerManager.Instance.interactableTriggers[trigger];
					
					noticeController.transform.position = cam.WorldToScreenPoint(trigger.position);
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
