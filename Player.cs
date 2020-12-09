using System;
using UnityEngine;

namespace Asteroids
{
    internal sealed class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _hp;
        [SerializeField] private Rigidbody2D _bullet;
        [SerializeField] private Transform _barrel;
        [SerializeField] private float _force;
        [SerializeField] private float _damage;
        private HpController _hpController;
        private Camera _camera;
        private Ship _ship;

        private void Start()
        {
            _hpController = new HpController(_hp);
            _hpController.Damage = _damage;
            _hpController.OnPlayerDead += DestroyPlayer; //Стоит вынести метод в отдельный класс?
            _hpController.OnHpChanged += _hpController._hpModel.ReduceHp; //НЕ ЗНАЮ, ИМЕЕТ ЛИ СМЫСЛ ИСПОЛЬЗОВАТЬ ДЛЯ ЭТОГО СОБЫТИЕ.
                                                                          //И, где должен быть метод уменьшения хп? В модели или контроллере?
                                                                          //Имеет ли смысл добавлять методы в событие в классе, где оно объявлено (HpController), или это будет неправильным использоованием события? 
            _camera = Camera.main;
            var moveTransform = new AccelerationMove(transform, _speed, _acceleration);
            var rotation = new RotationShip(transform);
            _ship = new Ship(moveTransform, rotation);
        }

        private void Update()
        {
            var direction = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
            _ship.Rotation(direction);
            
            _ship.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _ship.AddAcceleration();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _ship.RemoveAcceleration();
            }

            if (Input.GetButtonDown("Fire1"))
            {
                var temAmmunition = Instantiate(_bullet, _barrel.position, _barrel.rotation);
                temAmmunition.AddForce(_barrel.up * _force);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            _hpController.Interaction();
        }

        private void DestroyPlayer(object sender, EventArgs eventArgs)
        {
            Destroy(gameObject);
        }

    }
}
