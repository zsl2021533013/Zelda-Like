using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controller.UI
{
	// Generate Id:c7834660-a719-4ed0-8eaf-f745f145c948
	public partial class DialoguePanel
	{
		public const string Name = "DialoguePanel";
		public CanvasGroup rootCanvasGroup;
		public CanvasGroup responsePanelCanvasGroup;
		public TMP_Text dialogueContent;
		public NextButtonController nextDialogueController;
		public Transform responseContainer;

		private DialoguePanelData mPrivateData = null;
		private GameObject responseBtnTemplate;

		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public DialoguePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		DialoguePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new DialoguePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
