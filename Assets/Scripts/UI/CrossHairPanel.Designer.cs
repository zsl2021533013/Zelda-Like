using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:4ae36ab8-8531-4795-bfe2-7e4e2a4e3e88
	public partial class CrossHairPanel
	{
		public const string Name = "CrossHairPanel";

		public CanvasGroup canvasGroup;
		
		
		private CrossHairPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public CrossHairPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		CrossHairPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new CrossHairPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
