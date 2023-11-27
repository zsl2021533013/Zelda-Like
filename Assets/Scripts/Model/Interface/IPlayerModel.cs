using Controller.Character.Player.Player;
using QFramework;
using UnityEngine;

namespace Model.Interface
{
    public interface IPlayerModel : IModel
    {
        public Components components { get; }

        public void RegisterPlayer(params Object[] args);
        public void UnregisterPlayer();
    }
}