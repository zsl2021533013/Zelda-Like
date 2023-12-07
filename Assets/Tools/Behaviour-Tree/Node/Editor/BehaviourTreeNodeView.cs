using System.Linq;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;

namespace Tools.Behaviour_Tree.Node.Editor
{
    [NodeCustomEditor(typeof(BehaviourTreeNode))]
    public class BehaviourTreeNodeView : BaseNodeView
    {
        public void UpdateColor()
        {
            SetNodeColor(nodeTarget.color);
        }

        public override void OnRemoved()
        {
            if (inputPortViews.Count > 0)
            {
                var inputEdges = inputPortViews[0].GetEdges();
                for (var i = 0; i < inputEdges.Count(); i++)
                {
                    var edge = inputEdges[i];
                    owner.Disconnect(edge);
                }
            }
            
            if (outputPortViews.Count > 0)
            {
                var outputEdges = outputPortViews[0].GetEdges();
                for (var i = 0; i < outputEdges.Count(); i++)
                {
                    var edge = outputEdges[i];
                    owner.Disconnect(edge);
                }
            }
        }
    }
}