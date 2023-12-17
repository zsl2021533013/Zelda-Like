using QFramework;
using QFramework.Example;

namespace Level_Editor.Runtime.Action
{
    public class CloseNoticeAction : ActionBase
    {
        public override void OnEnter()
        {   
            UIKit.GetPanel<NoticePanel>().TryClose();
        }
    }
}