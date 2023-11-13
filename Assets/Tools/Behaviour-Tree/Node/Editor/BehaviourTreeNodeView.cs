using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;

namespace Behaviour_Tree.Node.Editor
{
    [NodeCustomEditor(typeof(BehaviourTreeNode))]
    public class BehaviourTreeNodeView : BaseNodeView
    {
        public void UpdateColor()
        {
            SetNodeColor(nodeTarget.color);
        }
    }
}