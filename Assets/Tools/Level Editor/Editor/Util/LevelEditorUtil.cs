using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Level_Editor.Runtime;
using Sirenix.Utilities;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

#endif   

namespace Level_Editor.Editor.Util
{
    public class LevelEditorPathMgr
    {
        public const string RootPath = "Assets/Tools/Level Editor";

        public const string EditorFolder = RootPath + "/Editor";

        public const string EditorUIFolder = EditorFolder + "/UI";

        public const string LevelEditorWindowFolder = EditorUIFolder + "/Level Editor Window";
        public const string NewSceneWindowFolder = EditorUIFolder + "/New Scene Window";
        
        public const string ResourceFolder = RootPath + "/Resources";
        
        public const string DataFolder = ResourceFolder + "/Data";
        public const string LevelDataPath = DataFolder + "/LevelEditorData.asset";
        
        public const string PrefabFolder = ResourceFolder + "/Prefab";
        public const string GridPath = PrefabFolder + "/Grid.prefab";

        public const string SceneFolder = ResourceFolder + "/Scenes";
    }
    
    public static class LevelEditorUtil
    {
        public static void EnsureLevelEditorData()
        {
#if UNITY_EDITOR
            if (!File.Exists(LevelEditorPathMgr.LevelDataPath))
            {
                var data = ScriptableObject.CreateInstance<LevelEditorData>();
                AssetDatabase.CreateAsset(data, LevelEditorPathMgr.LevelDataPath);
                AssetDatabase.SaveAssets();
            }
#endif           
        }
        
        public static LevelEditorData GetLevelEditorData()
        {
            EnsureLevelEditorData();
            
            var data = AssetDatabase.LoadAssetAtPath<LevelEditorData>(LevelEditorPathMgr.LevelDataPath);
            return data;
        }

        public static void AddLevelDatas(IEnumerable<LevelData> levelDatas)
        {
            levelDatas.ForEach(AddLevelData);
        }

        public static void AddLevelData(LevelData levelData)
        {
            var data = GetLevelEditorData();
            
            if(data.levelDatas.Any(data1 => data1.sceneField.SceneName == levelData.sceneField.SceneName))
            {
                return;
            }
            
            data.levelDatas.Add(levelData);
        }

        public static void SaveLevelEditorData()
        {
            var data = GetLevelEditorData();
            EditorUtility.SetDirty(data);
            Debug.Log("Level Editor Saved!");
        }

        public static void SaveLevelData(LevelData levelData)
        {
            var sceneField = levelData.sceneField;
            var scene = SceneManager.GetSceneByName(sceneField.SceneName);
            levelData.Save(scene);
        }

        public static void OpenLevels(IEnumerable<LevelData> levelDatas)
        {
            levelDatas.ForEach(OpenLevel);
        }
        
        public static void OpenLevel(LevelData levelData)
        {
            OpenScene(levelData.sceneField);
            
            EnsureLevel(levelData);
        }
        
        public static void CloseLevels(IEnumerable<LevelData> levelDatas)
        {
            levelDatas.ForEach(CloseLevel);
        }
        
        public static void CloseLevel(LevelData levelData)
        {
            CloseScene(levelData.sceneField);
        }
        
        public static void OpenScenes(IEnumerable<SceneField> sceneFields)
        {
            sceneFields.ForEach(OpenScene);
        }
        
        public static void OpenScene(SceneField sceneField)
        {
            var scene = SceneManager.GetSceneByName(sceneField.SceneName);
            if (scene is not { isLoaded: true })
            {
                EditorSceneManager.OpenScene(sceneField.ScenePath, OpenSceneMode.Additive);
            }
        }
        
        public static void CloseScenes(IEnumerable<SceneField> sceneFields)
        {
            sceneFields.ForEach(CloseScene);
        }
        
        public static void CloseScene(SceneField sceneField)
        {
            var scene = SceneManager.GetSceneByName(sceneField.SceneName);
            if (scene is { isLoaded: true })
            {
                EditorSceneManager.CloseScene(scene, true);
            }
        }

        public static void EnsureLevel(LevelData levelData)
        {
            var sceneField = levelData.sceneField;
            var scene = SceneManager.GetSceneByName(sceneField.SceneName);
            var root = scene.GetRootGameObjects();
            
            var grid = root.FirstOrDefault(o => o.name == "Grid" && o.GetComponent<Grid>());
            if (grid == null)
            {
                grid = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(LevelEditorPathMgr.GridPath));
                grid.name = "Grid";
                SceneManager.MoveGameObjectToScene(grid, scene);
                EditorSceneManager.SaveScene(scene);
            }
        }

        public static void SetFocus(LevelData levelData)
        {
            var sceneField = levelData.sceneField;
            var scene = SceneManager.GetSceneByName(sceneField.SceneName);
            var root = scene.GetRootGameObjects();
            var grid = root.FirstOrDefault(o => o.name == "Grid" && o.GetComponent<Grid>());

            if (grid)
            {
                Selection.activeObject = grid;
                SceneView.lastActiveSceneView.FrameSelected();
            }
        }
    }
}