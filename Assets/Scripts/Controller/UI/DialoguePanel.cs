using System.Collections.Generic;
using System.Linq;
using QFramework;
using TMPro;
using Tools.Dialogue_Graph.Node.Runtime.Core;
using Tools.Dialogue_Graph.Runtime.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.UI
{
	public class DialoguePanelData : UIPanelData
	{
	}
	
	public partial class DialoguePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as DialoguePanelData ?? new DialoguePanelData();

			responseBtnTemplate = Resources.Load<GameObject>("UI/Response Button");
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
		
		public void InitDialoguePanel(DialogueGraphNode contentNode, List<DialogueGraphNode> responseNodes)
		{
			nextDialogueBtn.onClick.RemoveAllListeners();
			var tmp = responseContainer.Cast<Transform>().ToList();
			tmp.ForEach(child => Destroy(child.gameObject));

			if (contentNode == null && responseNodes is not { Count: > 0 })
			{
				CloseSelf();
				return;
			}

			#region Dialogue Content

			if (contentNode != null)
			{
				dialogueContent.text = contentNode.text;
				nextDialogueBtn.onClick.AddListener(() =>
				{
					DialogueManger.Instance.ProcessNode(contentNode);
				});
			}
			
			#endregion

			#region Response 

			if (responseNodes is { Count: > 0 })
			{
				foreach (var node in responseNodes)
				{
					var button = Instantiate(responseBtnTemplate, responseContainer);

					/*button.GetComponent<RectTransform>().LocalScale(Vector3.one);*/

					button.GetComponentInChildren<TMP_Text>().text = node.text;
					button.GetComponent<Button>().onClick.AddListener(() =>
					{
						DialogueManger.Instance.ProcessNode(node);
					});
				}
			}
			
			#endregion
		}
	}
}
