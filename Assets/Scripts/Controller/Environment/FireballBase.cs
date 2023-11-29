using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Combat;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Timer = Script.View_Controller.Character_System.HFSM.Util.Timer;

namespace Controller.Environment
{
    public abstract class FireballBase : MonoBehaviour, IController, ITimeStop
    {
        [SerializeField, BoxGroup("Config")] protected bool enable;
        [SerializeField, BoxGroup("Config")] protected float speed;
        [SerializeField, BoxGroup("Config")] protected Vector3 direction;

        protected Vector3 startPos;
        
        private void OnEnable()
        {
            startPos = transform.position;
            
            this.GetModel<IFireballModel>()
                .RegisterFireball(transform);
            
            this.GetModel<IFireballModel>()
                .GetComponents(transform)
                .Add<FireballBase>(this);
            // 此处要指定添加的元素类型才行，不可使用 Add()，因为其会自动添加子类类型
            
            Debug.Log("Fireball Register!");
        }
        
        private void OnDisable()
        {
            this.GetModel<IFireballModel>().UnregisterFireball(transform);
            
            Debug.Log("Fireball Unregister");
        }

        public void Update()
        {
            if (Vector3.Distance(transform.position, startPos) > 50f)
            {
                Destroy(gameObject);
                enable = false;
                return;
            }
            
            if (!enable)
            {
                return;
            }
            
            transform.position += direction * (speed * Time.deltaTime);
            
            var colliders = DetectCollision();
            if (colliders.Count > 0)
            {
                OnCollision(colliders);
            }
        }

        public virtual void Init(Vector3 target, params Object[] args)
        {
            direction = (target - transform.position).normalized;
        }
        
        public virtual void OnCollision(List<Collider> colliders) {}

        public virtual void TimeStop() {}
        
        public virtual void TimeReset() {}

        private List<Collider> DetectCollision()
        {
            return Physics.OverlapBox(transform.position, transform.localScale / 2f)
                .Where(col => col.transform != transform)
                .ToList();
        }
        
        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}