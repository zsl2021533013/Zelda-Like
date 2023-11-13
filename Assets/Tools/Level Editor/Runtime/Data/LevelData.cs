using System;
using System.Linq;
using Level_Editor.Extension;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Level_Editor.Runtime
{
    public enum LoadState
    {
        Unloaded = 1,
        Loaded = 1 << 1,
        Unloading = 1 << 2,
        Loading = 1 << 3
    }
    
    [Serializable]
    public class LevelData
    {
        public SceneField sceneField;
        public Rect bounds;

        [NonSerialized] 
        public bool AOI = false;
        [NonSerialized]
        public LoadState LoadState = LoadState.Unloaded;

        public void Save(Scene scene)
        {
            if (scene.name != sceneField.SceneName)
            {
                return;
            }
            
            var gridRoot = scene.GetRootGameObjects().FirstOrDefault(o => o.name == "Grid");
            
            if (gridRoot == null)
            {
                Debug.LogError($"{sceneField.SceneName} has not initialized yet!");
                return;
            }
            
            var tilemaps = gridRoot.GetComponentsInChildren<Tilemap>();

            tilemaps.ForEach(tilemap => tilemap.CompressBounds());
            
            var tilemapRect = tilemaps.Select(tilemap =>
            {
                var bounds = tilemap.cellBounds;
                var minPos = new Vector2(bounds.min.x, bounds.min.y);
                var maxPos = new Vector2(bounds.max.x, bounds.max.y);
                
                return new Rect(minPos, maxPos - minPos);
            }).ToList();

            bounds = tilemapRect.FirstOrDefault();
            tilemapRect.ForEach(rect => bounds = bounds.Union(rect));
            
            Debug.Log($"{sceneField.SceneName} save successfully!");
        }
    }
}