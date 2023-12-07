using Tools.Behaviour_Tree.Runtime.Data;
using Tools.Behaviour_Tree.Runtime.Processor;
using UnityEngine;

namespace Tools.Behaviour_Tree.Runtime.Controller
{
    public class RuntimeBehaviourTree : MonoBehaviour
    {
        [Header("Behaviour Tree")]
        public BehaviourTreeGraph graph;
        
        private BehaviourTreeProcess _process;

        private void Awake()
        {
            graph.GetExposedParameter("A").value = "Hello World";
            graph.GetExposedParameter("B").value = "See You!";
        }

        private void Start()
        {
            _process ??= new BehaviourTreeProcess(graph);
        }

        private void Update()
        {
            _process.Update();
        }
    }
}