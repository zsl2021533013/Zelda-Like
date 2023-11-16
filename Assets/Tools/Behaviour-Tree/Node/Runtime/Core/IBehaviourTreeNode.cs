namespace Behaviour_Tree.Node.Runtime.Core
{
    public enum Status
    {
        Success,
        Failure,
        Running
    }
    
    public interface IBehaviourTreeNode
    {
        public void OnAwake();
        public void OnStart();
        public Status OnUpdate();
        public void OnStop();
    }
}