using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;

namespace QFramework.Example
{
	// Generate Id:e4f201d3-6860-4b45-9c2a-6d87376c0574
	public partial class NoticePanel
	{
		public const string Name = "NoticePanel";

		public CanvasGroup canvasGroup;
		public TMP_Text content;
		
		private NoticePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public NoticePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		NoticePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new NoticePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
