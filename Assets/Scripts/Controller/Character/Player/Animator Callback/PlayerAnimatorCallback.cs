using Controller.Character.Player.Combat;
using Controller.Character.Player.Player;
using Model.Interface;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controller.Character.Player.Animator_Callback
{
    public class PlayerAnimatorCallback : MonoBehaviour, IController
    {
        [ChildGameObjectsOnly] public PlayerWeaponController weaponController;
        
        public void StartParry()
        {
            var model = this.GetModel<IPlayerModel>();
            var status = model.components.Get<PlayerStatus>();
            status.isParrying.Set(true);
        }

        public void EndParry()
        {
            var model = this.GetModel<IPlayerModel>();
            var status = model.components.Get<PlayerStatus>();
            status.isParrying.Reset();
        }
        
        public void OpenWeapon()
        {
            weaponController.OpenWeapon();
        }

        public void CloseWeapon()
        {
            weaponController.CloseWeapon();
        }

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}
