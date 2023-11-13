using Behaviour_Tree.Runtime;
using GraphProcessor;
using UnityEditor;

namespace Behaviour_Tree.Editor
{
    [CustomEditor(typeof(BehaviourTreeGraph), false)]
    public class BehaviourTreeInspector : GraphInspector
    {
        protected override void CreateInspector()
        {
            base.CreateInspector();

        }
    }
}