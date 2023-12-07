using Tools.Dialogue_Graph.Runtime;
using Tools.Dialogue_Graph.Runtime.Data;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Tools.Dialogue_Graph.Editor
{
    public class DialogueGraphCallback
    {
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            var asset = EditorUtility.InstanceIDToObject(instanceID) as DialogueGraph;

            if (asset != null)
            {
                EditorWindow.GetWindow<DialogueGraphWindow>().InitializeGraph(asset);
                return true;
            }
            return false;
        }
    }
}