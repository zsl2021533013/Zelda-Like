using System.Linq;
using Command;
using Controller.Combat;
using Level_Editor.Runtime;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Timer = Script.View_Controller.Character_System.HFSM.Util.Timer;

namespace Controller.Environment
{
    public class PlayerFireballController : MonoBehaviour, IController, IStopped
    {
        [SerializeField, BoxGroup("Config")] private bool enable;
        [SerializeField, BoxGroup("Config")] private float speed;
        [SerializeField, BoxGroup("Config")] private Vector3 direction;
        
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

        public void InitFireball(Vector3 target)
        {
            direction = (target - transform.position).normalized;
        }

        private void DetectCollision()
        {
            var colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f)
                .Where(col => col.transform != transform && !col.CompareTag("Player"))
                .ToList();

            if (colliders.Count > 0)
            {
                enable = false;
                Destroy(gameObject);

                var trigger = colliders.FirstOrDefault(col => col.CompareTag("Trigger"));
                if (trigger)
                {
                    var controller = trigger.transform.GetComponent<TriggerController>();
                    controller.tryTrigger?.Invoke();
                }
            }
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