using System;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public Transform enemy;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var status = this.GetModel<IEnemyModel>().GetComponents(enemy).Get<EnemyStatus>();
            status.isAlert.Value = true;
        }
    }

    public IArchitecture GetArchitecture()
    {
        return ZeldaLike.Interface;
    }
}