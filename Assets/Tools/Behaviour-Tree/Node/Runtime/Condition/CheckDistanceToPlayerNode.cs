﻿using System;
using Behaviour_Tree.Node.Runtime.Core;
using GraphProcessor;
using Tools.Behaviour_Tree.Utils;
using UnityEngine;

namespace Tools.Behaviour_Tree.Node.Runtime.Condition
{
    
    [Serializable, NodeMenuItem("Behaviour/Condition/Check Distance To Player")]
    public class CheckDistanceToPlayerNode : EnemyConditionNode
    {
        public enum CheckDistanceType
        {
            More,
            Less
        }
        
        public enum CheckDistanceValue
        {
            None,
            AttackDist,
            PovDist
        }
        
        [ShowInInspector] 
        public CheckDistanceType type = CheckDistanceType.Less;
        
        [ShowInInspector] 
        public CheckDistanceValue value = CheckDistanceValue.None;
        
        private float distance;
        private Transform playerTrans;
        
        public override void OnAwake()
        {
            base.OnAwake();
            
            switch (value)
            {
                case CheckDistanceValue.None:
                    distance = 0f;
                    break;
                case CheckDistanceValue.AttackDist:
                    distance = config.attackDist;
                    break;
                case CheckDistanceValue.PovDist:
                    distance = config.povDist;
                    break;
                default:
                    distance = 0f;
                    break;
            }
        }

        public override void OnStart()
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override Status OnUpdate()
        {
            var dis = Vector3.Distance(transform.position, playerTrans.position);

            switch (type)
            {
                case CheckDistanceType.More:
                    return dis > distance ? Status.Success : Status.Failure;
                case CheckDistanceType.Less:
                    return dis <= distance ? Status.Success : Status.Failure;
                default:
                    return dis <= distance ? Status.Success : Status.Failure;
            }
        }
    }
}