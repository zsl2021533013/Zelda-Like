using System;
using System.Linq;
using Level_Editor.Editor.Util;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public class LevelBoundsDrawer : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            var data = LevelUtil.GetLevelEditorData();
            var levelData = data.levelDatas.FirstOrDefault(data1 => data1.sceneField.SceneName == gameObject.scene.name);

            if (levelData != null)
            {
                Gizmos.DrawWireCube(levelData.bounds.center, 
                    new Vector3(levelData.bounds.width, levelData.bounds.height, 0));
            }
        }
    }
}