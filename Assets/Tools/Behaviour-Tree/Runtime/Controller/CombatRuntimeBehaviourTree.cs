using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behaviour_Tree.Node.Runtime.Core;
using Behaviour_Tree.Runtime;
using Behaviour_Tree.Runtime.Processor;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using UnityEngine.AI;

public class CombatRuntimeBehaviourTree : MonoBehaviour
{
    [BoxGroup("Behaviour Tree")]
    public BehaviourTreeGraph graph;
    
    [FoldoutGroup("Components")] public Animator animator;
    [FoldoutGroup("Components")] public NavMeshAgent agent;
        
    private BehaviourTreeProcess _process;
    
    private Dictionary<Type, Component> components = new Dictionary<Type, Component>();

    private void Awake()
    {
        agent.updatePosition = false;
        agent.updateRotation = false;
        agent.angularSpeed = 1000f;
        
        components
            .Add<Transform>(transform)
            .Add<Animator>(animator)
            .Add<NavMeshAgent>(agent);
        
        graph.nodes.ForEach(node =>
        {
            if (node is BehaviourTreeNode treeNode)
            {
                treeNode.components = components;
                treeNode.OnAwake();
            }
        });
    }
    
    private void Start()
    {
         _process ??= new BehaviourTreeProcess(graph);
    }

    private void Update()
    {
        _process.Update();
    }
    
    protected virtual void OnAnimatorMove()
    {
        var position = animator.rootPosition;
        var nextPosition = agent.nextPosition;
        
        nextPosition = new Vector3(position.x, nextPosition.y, position.z);
        agent.nextPosition = nextPosition;
        position.y = nextPosition.y;
        transform.position = position;
            
        var rotation = animator.rootRotation;
        transform.rotation = rotation;
    }
}
