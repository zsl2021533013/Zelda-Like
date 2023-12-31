﻿namespace Tools.Behaviour_Tree.Node.Runtime.Core
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }
    
    public interface IBehaviourTreeNode
    {
        public void OnEnable();
        public void OnStart();
        public Status OnUpdate();
        public void OnStop();
    }
}