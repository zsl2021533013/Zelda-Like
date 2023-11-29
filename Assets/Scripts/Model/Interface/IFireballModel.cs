using System.Collections.Generic;
using Data.Character.Enemy;
using QFramework;
using UnityEngine;

namespace Model.Interface
{
    public interface IFireballModel : IModel
    {
        public Dictionary<Transform, Components> fireballDict { get; }

        public void RegisterFireball(Transform transform, params Object[] args);

        public void UnregisterFireball(Transform transform);

        public Components GetComponents(Transform transform);
    }
}