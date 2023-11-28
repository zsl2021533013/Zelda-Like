using System;
using System.Linq;
using Command;
using Controller.Combat;
using QFramework;
using Script.View_Controller.Character_System.HFSM.Util;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using Timer = Script.View_Controller.Character_System.HFSM.Util.Timer;

namespace Controller.Environment
{
    public class EnemyFireballController : MonoBehaviour, IController, IParried, IStopped
    {
        [SerializeField, BoxGroup("Config")] private bool enable;
        [SerializeField, BoxGroup("Config")] private float speed;
        [SerializeField, BoxGroup("Config")] private Vector3 direction;
        [SerializeField, BoxGroup("Config")] private Transform attacker;

        private Timer timer;

        private void OnEnable()
        {
            timer = new Timer();
        }

        private void Update()
        {
            if (timer > 30f)
            {
                Destroy(gameObject);
                enable = false;
                return;
            }
            
            if (!enable)
            {
                return;
            }
            
            DetectCollision();
            
            transform.position += direction * (speed * Time.deltaTime);
        }

        public void InitFireball(Transform attacker, Vector3 target)
        {
            this.attacker = attacker;
            direction = (target - transform.position).normalized;
        }

        private void DetectCollision()
        {
            var colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f)
                .Where(col => col.transform != transform)
                .ToList();

            if (colliders.Count > 0)
            {
                enable = false;
                Destroy(gameObject);

                if (colliders.FirstOrDefault(col => col.CompareTag("Player")))
                {
                    this.SendCommand(new TryHurtPlayerCommand() { attacker = this });
                    Debug.Log("Detect Player");
                }
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

        public void Stopped()
        {
            
        }
        
        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}