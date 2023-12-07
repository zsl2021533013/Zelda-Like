using GraphProcessor;
using Tools.Behaviour_Tree.Runtime.Data;
using UnityEditor;

namespace Tools.Behaviour_Tree.Editor
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