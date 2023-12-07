using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:e3b58241-7f71-43cb-b04d-19ee82d17f37
	public partial class TriggerPanel
	{
		public const string Name = "TriggerPanel";
		
		
		private TriggerPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public TriggerPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		TriggerPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new TriggerPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
