using System.Collections;
using System.Collections.Generic;
using Controller.Character.Enemy;
using Controller.Combat;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyAnimatorCallback : MonoBehaviour, IController
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

    public IArchitecture GetArchitecture()
    {
        return ZeldaLike.Interface;
    }
}
