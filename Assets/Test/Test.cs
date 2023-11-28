using System;
using Command;
using Controller.Environment;
using Data.Character.Enemy;
using Model.Interface;
using QFramework;
using UnityEngine;

public class Test : MonoBehaviour, IController
{
    public Transform player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            this.SendCommand(new SpawnEnemyFireballCommand()
            {
                position = transform.position,
                rotation = Quaternion.LookRotation((player.position - transform.position).normalized, Vector3.up),
                attacker = transform,
                target = player.position
            });
        }
    }

    public IArchitecture GetArchitecture()
    {
        return ZeldaLike.Interface;
    }
}