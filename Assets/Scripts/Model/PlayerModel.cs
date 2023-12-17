using Controller.Character.Player.Player;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Model
{
    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public enum AbilityType
        {
            Fireball,
            TimeStop
        }
        
        public Components components { get; private set; } = new Components();
        
        protected override void OnInit()
        {
            
        }
        
        public void RegisterPlayer(params Object[] args)
        {
            args.ForEach(arg =>
            {
                components.Add(arg);
            });

            components.Add(ScriptableObject.CreateInstance<PlayerStatus>());
            
            Debug.Log("Player Has Been Registered!");
        }
        
        public void UnregisterPlayer()
        {
            components = null;
            
            Debug.Log("Player Has Been Unregistered!");
        }
    }
}