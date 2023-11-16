using System.Collections;
using System.Collections.Generic;
using Behaviour_Tree.Runtime;
using Behaviour_Tree.Runtime.Processor;
using UnityEngine;

public class CombatRuntimeBehaviourTree : MonoBehaviour
{
    [Header("Behaviour Tree")]
    public BehaviourTreeGraph graph;

    public Animator animator;
        
    private BehaviourTreeProcess _process;

    private void Awake()
    {
        graph.GetExposedParameter("Animator").value = animator;
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
