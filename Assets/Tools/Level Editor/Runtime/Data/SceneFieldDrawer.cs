using System;
using Level_Editor.Runtime;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer : PropertyDrawer 
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, GUIContent.none, property);
        var sceneAsset = property.FindPropertyRelative("sceneAsset");
        var sceneName = property.FindPropertyRelative("sceneName");
        var scenePath = property.FindPropertyRelative("scenePath");
		
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        if (sceneAsset != null)
        {
            sceneAsset.objectReferenceValue = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false); 

            if( sceneAsset.objectReferenceValue != null )
            {
                sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset)?.name;
                scenePath.stringValue = AssetDatabase.GetAssetPath(sceneAsset.objectReferenceValue);
            }
        }
        EditorGUI.EndProperty();
    }
}
#endif