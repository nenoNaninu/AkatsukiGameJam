using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Neno.Scripts
{
    interface IEnemy
    {
        //爆発するときにはEnemiManagerのCurrentEnemyNumを減らすこと(えぇ…。
        void Explode();

        //すでにつながっているかどうか
        bool Combined { get; set; }

        EnemyType EnemyType { get; set; }

        //繋がった時の処理
        void OnLink();
    }
}
