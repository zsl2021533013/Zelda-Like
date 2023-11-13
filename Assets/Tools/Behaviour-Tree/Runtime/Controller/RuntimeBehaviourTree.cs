using System;
using Behaviour_Tree.Runtime.Processor;
using UnityEditor;
using UnityEngine;

namespace Behaviour_Tree.Runtime
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