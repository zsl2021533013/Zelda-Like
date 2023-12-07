using System;
using System.Collections.Generic;
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
		public UnityEvent onUpdate = new UnityEvent();
		private Transform player;
		private List<(Transform, Transform)> triggers = new ();

		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TriggerPanelData ?? new TriggerPanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
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
			player = this.GetModel<IPlayerModel>().components.Get<Transform>();
			/*onUpdate?.Invoke();*/
			ShowTriggers();
		}

		public void AddTrigger(Transform trigger)
		{
			triggers.Add((trigger, null));
			Debug.Log("Add Trigger");
		}

		public void ShowTriggers()
		{
			for (var i = 0; i < triggers.Count; i++)
			{
				var (trigger, ui) = triggers[i];
				
				if (ui == null)
				{
					var tmp = Resources.Load<GameObject>("Prefab/Far Trigger Notice")
						.Instantiate()
						.Parent(transform)
						.transform;
					ui = tmp;
					
					triggers[i] = (trigger, ui);
				}
				
				var viewportPos = Camera.main.WorldToViewportPoint(trigger.position);

				var isVisible = (viewportPos.x is > 0 and < 1 && 
				                 viewportPos.y is > 0 and < 1 && 
				                 viewportPos.z > 0);

				ui.position = Camera.main.WorldToScreenPoint(trigger.position);
				
				ui.gameObject.SetActive(isVisible);
			}
		}

		public IArchitecture GetArchitecture()
		{
			return ZeldaLike.Interface;
		}
	}
}
