using Controller.Character.Player.Player;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Model
{
    public class PlayerModel : AbstractModel, IPlayerModel
    {
        public Transform transform { get; private set; }
        public PlayerController controller { get; private set; }
        
        protected override void OnInit()
        {
            
        }

        public void RegisterPlayer(Transform transform)
        {
            this.transform = transform;
            controller = transform.GetComponent<PlayerController>();
            
            Debug.Log("Player Has Been Registered!");
        }
        
        public void UnregisterPlayer()
        {
            transform = null;
            controller = null;
            
            Debug.Log("Player Has Been Unregistered!");
        }
    }
}