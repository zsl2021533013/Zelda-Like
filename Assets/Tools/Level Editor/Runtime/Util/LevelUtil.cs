using System.Collections.Generic;
using System.Linq;
using Level_Editor.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level_Editor.Editor.Util
{
    public class LevelPathMgr
    {
        public const string DataFolder = "Data";
        public const string LevelDataPath = DataFolder + "/LevelEditorData";
    }
    
    public static class LevelUtil
    {
        private class LevelCache
        {
            public int Capacity = 4;
            public List<LevelData> LevelDatas = new List<LevelData>();

            public void Add(LevelData levelData)
            {
                LevelDatas.Add(levelData);

                while (LevelDatas.Count > Capacity)
                {
                    if (LevelDatas.All(levelData => levelData.AOI))
                    {
                        Capacity <<= 1;
                        Debug.Log("Level Cache Resize!");
                    }

                    var exceptLevel = LevelDatas.FirstOrDefault(data => data.AOI == false);
                    if (exceptLevel != null)
                    {
                        LevelDatas.Remove(exceptLevel);
                        UnloadLevel(exceptLevel);
                    }
                }
            }
        }

        private static LevelCache _cache = new LevelCache();
        
        public static LevelEditorData GetLevelEditorData()
        {
            var data = Resources.Load<LevelEditorData>(LevelPathMgr.LevelDataPath);
            return data;
        }
        
        public static void LoadLevel(LevelData levelData)
        {
            if (levelData.LoadState is LoadState.Loaded or LoadState.Loading)
            {
                return;
            }

            var sceneField = levelData.sceneField;
            var sceneName = sceneField.SceneName;

            levelData.LoadState = LoadState.Loading;

            _cache.Add(levelData);
            var ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            ao.completed += operation => levelData.LoadState = LoadState.Loaded;
        }
        
        private static void UnloadLevel(LevelData levelData)
        {
            if (levelData.LoadState is LoadState.Unloaded or LoadState.Unloading)
            {
                return;
            }

            var sceneField = levelData.sceneField;
            var sceneName = sceneField.SceneName;

            levelData.LoadState = LoadState.Unloading;

            var ao = SceneManager.UnloadSceneAsync(sceneName);

            ao.completed += operation => levelData.LoadState = LoadState.Unloaded;
        }
    }
}