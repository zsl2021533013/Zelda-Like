﻿using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Combat;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controller.Environment
{
    public class EnemyFireballController : FireballBase, IParried
    {
        [SerializeField, BoxGroup("Config")] private Transform attacker;

        public override void Init(Vector3 target, params Object[] args)
        {
            base.Init(target, args);
            attacker = (Transform)args[0];
        }

        public override void OnCollision(List<Collider> colliders)
        {
            enable = false;
            Destroy(gameObject);

            if (colliders.FirstOrDefault(col => col.CompareTag("Player")))
            {
                this.SendCommand(new TryHurtPlayerCommand() { attacker = this });
                Debug.Log("Detect Player");
            }
        }

        public void Parried()
        {
            this.SendCommand(new SpawnPlayerFireballCommand()
            {
                position = transform.position,
                rotation = transform.rotation,
                target = attacker.position
            });
        }

        public override void TimeStop()
        {
            enable = false;
        }

        public override void TimeReset()
        {
            enable = true;
        }
    }
}