using Controller.Character.Player.Player;
using QFramework;
using UnityEngine;

namespace Model.Interface
{
    public interface IPlayerModel : IModel
    {
        public Transform transform { get; }
        public PlayerController controller { get; }

        public void RegisterPlayer(Transform transform);
        public void UnregisterPlayer();
    }
}