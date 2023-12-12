using System.Collections;
using System.Collections.Generic;
using Controller.Combat;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyAnimatorCallback : MonoBehaviour
{
    [ChildGameObjectsOnly] public EnemyWeaponController weaponController;

    public void OpenWeapon()
    {
        weaponController.OpenWeapon();
    }

    public void CloseWeapon()
    {
        weaponController.CloseWeapon();
    }
}
