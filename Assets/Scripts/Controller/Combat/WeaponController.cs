using System;
using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Character.Enemy;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Controller.Combat
{
    public class WeaponController : MonoBehaviour, IController
    {
        public bool isWeaponEnable = false;

        [ListDrawerSettings(CustomAddFunction = "OnPointAdd", CustomRemoveIndexFunction = "OnPointRemove")]
        public List<Transform> detectPoints;
        
        private void Update()
        {
            if (!isWeaponEnable)
            {
                return;
            }
            
            for (var i = 1; i < detectPoints.Count; i++)
            {
                var pos1 = detectPoints[i - 1].position; 
                var pos2 = detectPoints[i].position;
                
                var infos = DetectCollision(pos1, pos2);
                var detectPlayer = infos.Any(info => info.collider.CompareTag("Player"));

                if (detectPlayer)
                {
                    CloseWeapon();
                    
                    this.SendCommand(new TryHurtPlayerCommand()
                    {
                        attacker = this.GetModel<IEnemyModel>().enemyDict
                            .Values
                            .FirstOrDefault(components => components.Get<WeaponController>() == this)
                            ?.Get<EnemyController>(),
                        type = AttackType.Melee
                    });
                    
                    Debug.Log("Attack Player");
                }

                Debug.DrawLine(pos1, pos2, detectPlayer ? Color.red : Color.green, 0.1f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (var i = 1; i < detectPoints.Count; i++)
            {
                var pos1 = detectPoints[i - 1].position; 
                var pos2 = detectPoints[i].position;
                
                Gizmos.DrawLine(pos1, pos2);
            }
        }

        public void OnPointAdd()
        {
            var point = transform.Find($"Weapon Point {detectPoints.Count}")?.gameObject ? 
                transform.Find($"Weapon Point {detectPoints.Count}")?.gameObject : 
                new GameObject($"Weapon Point {detectPoints.Count}");
            point.Parent(transform);
            point.LocalPosition(Vector3.zero);

            detectPoints.Add(point.transform);
                
            Debug.Log("Add Points");
        } 
        
        public void OnPointRemove(int index)
        {
            var point = detectPoints[index].gameObject;
            
            DestroyImmediate(point);

            detectPoints.RemoveAt(index);
                
            Debug.Log("Remove Points");
        } 

        private static IEnumerable<RaycastHit> DetectCollision(Vector3 startPos, Vector3 endPos)
        {
            var ray = new Ray(startPos, endPos - startPos);

            var hitInfo = Physics.RaycastAll(ray, Vector3.Distance(startPos, endPos));
            return hitInfo;
        }
        
        public WeaponController OpenWeapon()
        { 
            isWeaponEnable = true;
            return this;
        }
        
        public WeaponController CloseWeapon()
        { 
            isWeaponEnable = false;
            return this;
        }

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}