using Sirenix.OdinInspector;
using UnityEngine;

namespace Controller.Character.Player.Player
{
    [CreateAssetMenu(fileName = "Player Config", menuName = "Scriptable Object/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        [FoldoutGroup("Ground")] public float walkSpeed;
        [FoldoutGroup("Ground")] public float runSpeed;

        [FoldoutGroup("Ground/Slop")] public float slopDetectFrontOffset;
        [FoldoutGroup("Ground/Slop")] public float slopDetectFrontDistance;
        [FoldoutGroup("Ground/Slop")] public float groundDetectDistance;
        [FoldoutGroup("Ground/Slop")] public float flatGroundMaxAngle;
        [FoldoutGroup("Ground/Slop")] public float slopMaxAngle;
        [FoldoutGroup("Ground/Slop")] public LayerMask groundLayerMask;
        [FoldoutGroup("Ground/Slop")] public PhysicMaterial fullFrictionMat;
        [FoldoutGroup("Ground/Slop")] public PhysicMaterial zeroFrictionMat;

        [FoldoutGroup("Jump")] public float jumpForce;
        
        [FoldoutGroup("Combat")] public float blockTime;
    }
}