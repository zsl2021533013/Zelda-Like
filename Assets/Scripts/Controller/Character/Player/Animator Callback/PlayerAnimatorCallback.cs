using Controller.Character.Player.Player;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Controller.Character.Player.Animator_Callback
{
    public class PlayerAnimatorCallback : MonoBehaviour, IController
    {
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

        public IArchitecture GetArchitecture()
        {
            return ZeldaLike.Interface;
        }
    }
}
