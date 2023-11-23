using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Behaviour_Tree.Node.Runtime.Core
{
    [Serializable]
    public abstract class BehaviourTreeNode : BaseNode, IBehaviourTreeNode
    {
        public override string name
        {
            get
            {
                var ans = GetType().Name;

                ans = ans[..^4];

                return Regex.Replace(ans, @"([A-Z])", " $1");
            }
        }

        public Dictionary<Type, Object> components;
        
        // 我们希望状态的初始值能在运行中修改，同时不在运行中保存，故使用 [NonSerialized]
        [NonSerialized] private Status _status = Status.Running;
        public Status status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value != _status)
                {
                    if (value == Status.Success)
                    {
                        _color = Color.green;
                    }
                    
                    _lastStatusChangeTime = Time.time;
                    _status = value;
                }
            }
        }

        [NonSerialized] private bool _hasStarted = false;
        public bool hasStarted => _hasStarted;
        
        // 距离上一次状态改变的时间
        [NonSerialized] private float _lastStatusChangeTime = 0f;
        
        // 颜色的更新在 EditorWindow OnInspectorUpdate 中进行，此处仅供读取
        [NonSerialized] private Color _color = Color.gray;
        public override Color color
        {
            get
            {
                if (!hasStarted && status == Status.Running)
                {
                    return _color = GetLerpColor(Color.gray);
                }
                
                switch (status)
                {
                    case Status.Success:
                        return _color = GetLerpColor(Color.grey);
                    case Status.Running:
                        return _color = Color.green;
                    case Status.Failure:
                        return _color = GetLerpColor(Color.red);
                    default:
                        return _color = GetLerpColor(Color.grey);
                }
            }
        }

        public Status Update()
        {
            if (!_hasStarted)
            {
                _hasStarted = true;
                
                // 说明上一次已经跑过了，这一次是新的一次，需要初始化一下状态
                // 直接修改 _status 是为了避免触发 state { get; } 中的相关代码
                _status = Status.Running;

                // 拉取黑板数据
                foreach (var inputNode in GetInputNodes().Where(node => node is not BehaviourTreeNode))
                {
                    inputNode.OnProcess();
                }
                OnProcess();
                
                OnStart();
            }

            status = OnUpdate();

            if (status is Status.Success or Status.Failure)
            {
                _hasStarted = false;
                OnStop();
            }

            return status;
        }

        public virtual void OnAwake() { }

        public virtual void OnStart() { }

        public abstract Status OnUpdate();

        public virtual void OnStop() { }
        
        public void Abort() 
        {
            this.Traverse(node => 
            {
                node._hasStarted = false;
                node._status = Status.Running;
                node.OnStop();
            });
        }

        private Color GetLerpColor(Color targetColor)
        {
            var currentTime = Time.time;
            var passTime = (currentTime - _lastStatusChangeTime) / 1f;
            
            passTime = Mathf.Clamp01(passTime);

            return Color.Lerp(_color, targetColor, passTime);
        }
    }
}