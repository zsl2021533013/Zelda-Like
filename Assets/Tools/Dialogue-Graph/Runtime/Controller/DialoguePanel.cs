using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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

			responseBtnTemplate = Resources.Load<GameObject>("UI/ResponseButton");
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			rootCanvasGroup.alpha = 0f;
			rootCanvasGroup.DOFade(1f, 0.3f);
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

		private void OnDisable()
		{
			rootCanvasGroup.DOKill();
		}

		public void InitDialoguePanel(DialogueGraphNode contentNode, List<DialogueGraphNode> responseNodes)
		{
			nextDialogueBtn.onClick.RemoveAllListeners();

			if (contentNode == null && responseNodes is not { Count: > 0 })
			{
				rootCanvasGroup.DOFade(0f, 0.3f).OnComplete(CloseSelf);
				return;
			}

			#region Dialogue Content

			if (contentNode != null)
			{
				dialogueContent.text = "";
				dialogueContent.DOText(contentNode.text, contentNode.text.Length * 0.1f).SetEase(Ease.Linear);
				nextDialogueBtn.onClick.AddListener(() =>
				{
					DialogueManger.Instance.ProcessNode(contentNode);
				});
			}
			
			#endregion

			#region Response 

			responsePanelCanvasGroup.DOFade(0f, 0.3f)
				.OnComplete(() =>
				{
					var tmp = responseContainer.Cast<Transform>().ToList();
					tmp.ForEach(child => Destroy(child.gameObject));

					responsePanelCanvasGroup.alpha = 1f;

					if (responseNodes is { Count: > 0 })
					{
						foreach (var node in responseNodes)
						{
							var button = Instantiate(responseBtnTemplate, responseContainer)
								.GetComponent<ResponseButton>();

							button.text.text = node.text;
							button.onClick.AddListener(() => { DialogueManger.Instance.ProcessNode(node); });
						}
					}
				});
			
			#endregion
		}
	}
}
