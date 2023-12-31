using QFramework;
using Tools.Dialogue_Graph.Runtime.Data;
using Tools.Dialogue_Graph.Runtime.Manager;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public DialogueGraph graph;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueManger.Instance.StartDialogue(graph);
        }
    }

    public IArchitecture GetArchitecture()
    {
        return ZeldaLike.Interface;
    }
}