using System;
using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Combat;
using Model.Interface;
using QFramework;
using Script.View_Controller.Character_System.HFSM.Util;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Timer = Script.View_Controller.Character_System.HFSM.Util.Timer;

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

        public override void Stopped()
        {
            enable = false;
        }
    }
}