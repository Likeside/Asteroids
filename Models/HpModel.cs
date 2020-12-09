using System;
using UnityEngine;

namespace Asteroids
{
    public class HpModel
    {
        public float Hp
        {
            get;
            set;
        }

        public HpModel(float hp)
        {
            this.Hp = hp;
        }

        public void ReduceHp(object sender, HpModelEventArgs eventArgs)
        {
            Hp -= eventArgs.Hp;
        }
        
    }
}