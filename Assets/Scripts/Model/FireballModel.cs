using System.Collections.Generic;
using Model.Interface;
using QFramework;
using UnityEngine;

namespace Model
{
    public class FireballModel : AbstractModel, IFireballModel
    {
        public Dictionary<Transform, Components> fireballDict { get; private set; } 
            = new Dictionary<Transform, Components>();
        
        protected override void OnInit()
        {
        }
        
        public void RegisterFireball(Transform transform, params Object[] args)
        {
            fireballDict.TryAdd(transform, new Components());

            fireballDict[transform].Add(transform);
            args.ForEach(arg => fireballDict[transform].Add(arg));
            
            Debug.Log($"{transform.name} Has Been Registered!");
        }

        public void UnregisterFireball(Transform transform)
        {
            if (fireballDict.ContainsKey(transform))
            {
                fireballDict.Remove(transform);
            }
            
            Debug.Log($"{transform.name} Has Been Unregistered!");
        }

        public Components GetComponents(Transform transform)
        {
            fireballDict.TryGetValue(transform, out var components);
            return components;
        }
    }
}