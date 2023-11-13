using GraphProcessor;
using UnityEditor;
using UnityEngine.UIElements;

namespace Behaviour_Tree.Editor
{
    public class BehaviourTreeToolBarView : ToolbarView
    {
        public BehaviourTreeToolBarView(BaseGraphView graphView) : base(graphView)
        {
        }
    
        protected override void AddButtons()
        {
            var exposedParamsVisible = 
                graphView.GetPinnedElementStatus<ExposedParameterView>() != DropdownMenuAction.Status.Hidden;
            AddToggle("Show Parameters", exposedParamsVisible, (v) => 
                graphView.ToggleView<ExposedParameterView>());
        }
    }
}