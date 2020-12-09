using System;
using UnityEngine;

namespace Asteroids
{
    public class HpController
    {
        public HpModel _hpModel;

        public float Damage // За количество наносимых повреждения отвечает HpController, в дальнейшем планирую перенести данный функционал на, непосредственно, объект, наносящий повреждения
        {
            get;
            set;
        }
        
        public event EventHandler<HpModelEventArgs> OnHpChanged;
        public event EventHandler OnPlayerDead;
        
        
        public HpController(float startingHp)
        {
            this._hpModel = new HpModel(startingHp); //Создание контроллера создает модель, это уместный подход?
        }

        public void Interaction()
        {
            
            OnHpChanged?.Invoke(this, new HpModelEventArgs(Damage));
            if (_hpModel.Hp <= 0)
            {
                OnPlayerDead?.Invoke(this, EventArgs.Empty);
            }
        }

       
    }
}