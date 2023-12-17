using QFramework;
using QFramework.Example;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Level_Editor.Runtime.Action
{
    public class ShowNoticeAction : ActionBase
    {
        [TextArea(4, 10)]
        public string text;

        public override void OnEnter()
        {
            UIKit.OpenPanel<NoticePanel>(new NoticePanelData() { text = text });
        }
    }
}