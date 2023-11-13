

using System.Linq;
using Level_Editor.Editor.Util;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Level_Editor.Editor
{
    [InitializeOnLoad]
    public static class LevelEditorInitializer
    {
        #if UNITY_EDITOR
        
        static LevelEditorInitializer()
        {
            /*EditorSceneManager.sceneSaved += SaveLevel;
            EditorSceneManager.sceneSaved += scene => LevelEditorUtil.SaveLevelEditorData();*/
        }
        
        #endif
        
        public static void SaveLevel(Scene scene)
        {
            var data = LevelEditorUtil.GetLevelEditorData();
            var levelData = data.levelDatas
                .FirstOrDefault(levelData => levelData.sceneField.SceneName == scene.name);
            
            if (levelData != null)
            {
                LevelEditorUtil.SaveLevelData(levelData);
            }
        }
    }
}