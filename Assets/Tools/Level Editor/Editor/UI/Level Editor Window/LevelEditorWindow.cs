using System;
using System.Linq;
using Level_Editor.Editor.Util;
using Level_Editor.Runtime;
using Sirenix.Utilities;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#endif

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("Tools/LevelEditorWindow")]
    public static void Open()
    {
        var window = GetWindow<LevelEditorWindow>();
        window.titleContent = new GUIContent("LevelEditorWindow");
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>
            (LevelEditorPathMgr.LevelEditorWindowFolder + "/LevelEditorWindow.uxml");
        var uxml = visualTree.Instantiate();
        uxml.style.flexGrow = 1f;
        root.Add(uxml);
        
        InitLevelList();
    }

    private void InitLevelList()
    {
        var root = rootVisualElement;

        var searchField = root.Q<ToolbarSearchField>("LevelSearchField");
        var listView = root.Q<ListView>("LevelListView");

        #region SearchField And ListView
        
        listView.fixedItemHeight = 25;
        listView.makeItem = () => new Label() { style = { unityTextAlign = TextAnchor.MiddleCenter } };
        listView.bindItem = (element, i) =>
        {
            var sceneFieldsList = listView.itemsSource
                .OfType<LevelData>()
                .Select(levelData => levelData.sceneField)
                .ToList();
            ((Label)element).text = sceneFieldsList[i].SceneName;
        };

        UpdateList();
        
        #endregion

        #region Manipulator
        
        var menuManipulator = new ContextualMenuManipulator(evt =>
        {
            evt.menu.AppendAction("New", action =>
            {
                NewSceneWindow.Open();
            });
            
            evt.menu.AppendAction("Open", action =>
            {
                var levelDatas = listView.selectedItems.OfType<LevelData>();
                LevelEditorUtil.OpenLevels(levelDatas);
                LevelEditorUtil.SetFocus(levelDatas.FirstOrDefault());
                levelDatas.ForEach(LevelEditorUtil.SaveLevelData);
            });
            
            evt.menu.AppendAction("Close", action =>
            {
                var levelDatas = listView.selectedItems.OfType<LevelData>();
                LevelEditorUtil.CloseLevels(levelDatas);
            });
            
            evt.menu.AppendAction("Delete", action =>
            {
                var data = LevelEditorUtil.GetLevelEditorData();
                
                var levelDatas = listView.selectedItems.OfType<LevelData>().ToList();

                LevelEditorUtil.CloseLevels(levelDatas);
                
                var newData = data.levelDatas.Except(levelDatas).ToList();

                data.levelDatas = newData;
                
                UpdateList();
            });
            
            evt.StopPropagation();
        });
        
        listView.AddManipulator(menuManipulator);

        #endregion

        #region Event Register

        searchField.RegisterCallback<ChangeEvent<string>>(evt =>
        {
            UpdateList();
        });
        
        listView.RegisterCallback<DragUpdatedEvent>(evt =>
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
        });
        
        listView.RegisterCallback<DragExitedEvent>(evt =>
        {
            var references = DragAndDrop.objectReferences.OfType<SceneAsset>();
            LevelEditorUtil
                .AddLevelDatas(references.Select(asset => new LevelData(){sceneField = new SceneField(asset)}));

            UpdateList();
        });
        
        listView.RegisterCallback<KeyDownEvent>(evt =>
        {
            
            if (evt.keyCode == KeyCode.S && evt.ctrlKey)
            {
                LevelEditorUtil.SaveLevelEditorData();
            }
        });

        listView.onItemsChosen += objects =>
        {
            var levelDatas = objects.OfType<LevelData>();

            LevelEditorUtil.OpenLevels(levelDatas);
            LevelEditorUtil.SetFocus(levelDatas.FirstOrDefault());
        };

        #endregion

    }
    
    public void UpdateList()
    {
        var data = LevelEditorUtil.GetLevelEditorData();
        
        var root = rootVisualElement;
        
        var searchField = root.Q<ToolbarSearchField>("LevelSearchField");
        var listView = root.Q<ListView>("LevelListView");
        
        var targetList = data.levelDatas
            .Where(data =>
            {
                var sceneField = data.sceneField;
                var target = searchField.value;
                var sceneName = sceneField.SceneName;
                return target == null || sceneName.Contains(target);
            })
            .ToList();

        targetList.Sort((x, y) => String.CompareOrdinal(x.sceneField.SceneName, y.sceneField.SceneName));
        
        listView.itemsSource = targetList;
    }
}
