using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class NoticePanelData : UIPanelData
	{
		public string text;
	}
	
	public partial class NoticePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as NoticePanelData ?? new NoticePanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			canvasGroup.DOFade(1f, 0.3f);
			content.text = mData.text;
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

		public void TryClose()
		{
			canvasGroup.DOKill();
			canvasGroup.DOFade(0f, 0.3f).OnComplete(CloseSelf);
		}
	}
}
