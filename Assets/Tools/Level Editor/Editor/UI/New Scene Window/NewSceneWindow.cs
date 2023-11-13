using Level_Editor.Editor.Util;
using Level_Editor.Runtime;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class NewSceneWindow : EditorWindow
{
    public static void Open()
    {
        var window = GetWindow<NewSceneWindow>();
        window.titleContent = new GUIContent("NewSceneWindow");
    }
    
    public void CreateGUI()
    {
        var root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            (LevelEditorPathMgr.NewSceneWindowFolder + "/NewSceneWindow.uxml");
        VisualElement labelFromUxml = visualTree.Instantiate();
        root.Add(labelFromUxml);

        var newSceneTextField = root.Q<TextField>("NewSceneTextField");
        var confirmBtn = root.Q<Button>("ConfirmBtn");
        var cancelBtn = root.Q<Button>("CancelBtn");

        confirmBtn.clicked += () =>
        {
            var sceneName = newSceneTextField.value;
            var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
            var path = LevelEditorPathMgr.SceneFolder + "/" + sceneName + ".unity";
            
            EditorSceneManager.SaveScene(newScene, path);
            
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            var levelData = new LevelData() { sceneField = new SceneField(sceneAsset) };
            
            LevelEditorUtil.AddLevelData(levelData);
            LevelEditorUtil.EnsureLevel(levelData);
            
            GetWindow<LevelEditorWindow>().UpdateList();

            Selection.activeObject = sceneAsset;
            
            Close();
        };
        cancelBtn.clicked += Close;
    }
}