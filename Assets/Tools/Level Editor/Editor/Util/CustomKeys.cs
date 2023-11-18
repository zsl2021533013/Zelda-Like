using System.Linq;
using UnityEditor;
using UnityEngine;

public class CustomKeys : Editor
{
    [MenuItem("GameObject/贴合地面 %E")]
    public static void FitTheGround()
    {
        var transform = Selection.gameObjects.FirstOrDefault()?.transform;
        var collider = transform.GetComponent<Collider>();

        if (!transform || !collider)
        {
            return;
        }

        var objectHeight = collider.bounds.extents.y;
        
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity))
        {
            transform.position = hit.point + new Vector3(0, objectHeight, 0);
        }
    }
}