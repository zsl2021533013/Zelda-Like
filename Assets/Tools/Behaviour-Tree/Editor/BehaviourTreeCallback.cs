using Behaviour_Tree.Runtime;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Behaviour_Tree.Editor
{
    public class BehaviourTreeCallback
    {
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID) as BehaviourTreeGraph;

            if (asset != null)
            {
                EditorWindow.GetWindow<BehaviourTreeGraphWindow>().InitializeGraph(asset);
                return true;
            }
            return false;
        }
    }
}