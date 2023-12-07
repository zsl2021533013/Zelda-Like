using System;
using System.Collections.Generic;
using System.Linq;
using Model.Interface;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
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
		private Dictionary<Transform, Transform> triggerUIDict = new Dictionary<Transform, Transform>();

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
			var triggers = TriggerManager.Instance.interactableTriggers;

			foreach (var trigger in triggers)
			{
				Transform ui = null;
				if (!triggerUIDict.ContainsKey(trigger))
				{
					ui = Resources.Load<GameObject>("Prefab/Far Trigger Notice")
						.Instantiate()
						.Parent(transform)
						.transform;
					
					triggerUIDict.Add(trigger, ui);
				}
				else
				{
					ui = triggerUIDict[trigger];
				}
				
				var viewportPos = cam.WorldToViewportPoint(trigger.position);
				var inDistance = Vector3.Distance(player.position, trigger.position) < 10f;
				
				var isVisible = (viewportPos.x is > 0 and < 1 && 
				                 viewportPos.y is > 0 and < 1 && 
				                 viewportPos.z > 0) && inDistance;
				
				ui.position = cam.WorldToScreenPoint(trigger.position);
				
				ui.gameObject.SetActive(isVisible);
			}
		}

		public IArchitecture GetArchitecture()
		{
			return ZeldaLike.Interface;
		}
	}
}
