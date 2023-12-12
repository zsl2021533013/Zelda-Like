using System.Collections.Generic;
using System.Linq;
using Command;
using Controller.Character.Enemy;
using Data.Combat;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controller.Character.Player.Combat
{
    public class PlayerWeaponController : MonoBehaviour, IController
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
                var targetInfo = infos.FirstOrDefault(info => info.collider.CompareTag("Enemy"));

                if (targetInfo.collider)
                {
                    CloseWeapon();
                    
                    this.SendCommand(new TryHurtEnemyCommand()
                    {
                        enemy = targetInfo.transform,
                        attackerData = this.GetModel<IPlayerModel>().components.Get<CharacterCombatData>(),
                        /*attackPoint = targetInfo.point*/
                    });
                    
                    Debug.Log("Attack Player");
                }

                Debug.DrawLine(pos1, pos2, targetInfo.collider ? Color.red : Color.green, 0.1f);
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
        
        public PlayerWeaponController OpenWeapon()
        { 
            isWeaponEnable = true;
            return this;
        }
        
        public PlayerWeaponController CloseWeapon()
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