using System;
using UnityEngine;

namespace Level_Editor.Extension
{
    public static class RectExtension
    {
        public static Rect Union(this Rect a, Rect b)
        {
            var union = Rect.MinMaxRect(
                Mathf.Min(a.xMin, b.xMin),
                Mathf.Min(a.yMin, b.yMin),
                Mathf.Max(a.xMax, b.xMax),
                Mathf.Max(a.yMax, b.yMax));
            
            return union;
        }
    }
}