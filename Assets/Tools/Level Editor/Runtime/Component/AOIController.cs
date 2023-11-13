using System;
using System.Linq;
using Level_Editor.Editor.Util;
using Sirenix.Utilities;
using UnityEngine;

namespace Level_Editor.Runtime
{
    public class AOIController : MonoBehaviour
    {
        public Vector2 Size => 10 * Vector2.one;
    
        public Rect Rect
        {
            get
            {
                var position = new Vector2(transform.position.x, transform.position.y);
                var minPos = position - Size / 2;
                return new Rect(minPos, Size);
            }
        }
        
        private void Update()
        {
            if (transform.hasChanged)
            {
                transform.hasChanged = false;

                var data = LevelUtil.GetLevelEditorData();

                data.levelDatas.ForEach(levelData => levelData.AOI = levelData.bounds.Overlaps(Rect));
            
                var intersectLevels = data.levelDatas.Where(levelData => levelData.bounds.Overlaps(Rect));

                intersectLevels.Where(levelData => levelData.LoadState is LoadState.Unloaded).ForEach(LevelUtil.LoadLevel);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Rect.center, new Vector3(Rect.width, Rect.height, 0));
        }
    }
}