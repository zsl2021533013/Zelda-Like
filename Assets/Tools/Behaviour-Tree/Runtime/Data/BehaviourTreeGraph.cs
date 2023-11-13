using System;
using GraphProcessor;
using UnityEngine;

namespace Behaviour_Tree.Runtime
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }
    
    [Serializable]
    [CreateAssetMenu(menuName = "Behaviour Tree", fileName = "Behaviour Tree")]
    public class BehaviourTreeGraph : BaseGraph
    {
    }
}