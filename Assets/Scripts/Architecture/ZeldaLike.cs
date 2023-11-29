using System.Collections;
using System.Collections.Generic;
using Model;
using Model.Interface;
using QFramework;
using UnityEngine;

public class ZeldaLike : Architecture<ZeldaLike>
{
    protected override void Init()
    {
        RegisterModel<IEnemyModel>(new EnemyModel());
        RegisterModel<IPlayerModel>(new PlayerModel());
        RegisterModel<IFireballModel>(new FireballModel());
    }
}
