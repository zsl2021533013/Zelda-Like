using System;
using System.Linq;
using GraphProcessor;
using Tools.Behaviour_Tree.Node.Runtime.Core;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Root
{
    [Serializable]
    public class RootNode : BehaviourTreeNode
    {
        [Output("child", false), Vertical, HideInInspector] public BehaviourTreeLink child;

        public override string layoutStyle => "Behaviour Tree/RootNodeStyle";

        public override bool deletable => false;

        public override Status OnUpdate()
        {
            if (GetOutputNodes().FirstOrDefault() is BehaviourTreeNode childNode)
            {
                return childNode.Update();
            }
            else
            {
                return Status.Success;
            }
        }
    }
}