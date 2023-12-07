using GraphProcessor;
using Tools.Dialogue_Graph.Runtime;
using Tools.Dialogue_Graph.Runtime.Data;
using UnityEditor;

namespace Tools.Dialogue_Graph.Editor
{
    public class DialogueGraphInspector
    {
        [CustomEditor(typeof(DialogueGraph), false)]
        public class BehaviourTreeInspector : GraphInspector
        {
            protected override void CreateInspector()
            {
                base.CreateInspector();
            }
        }
    }
}