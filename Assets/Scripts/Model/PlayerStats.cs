using System;
using UnityEngine;

namespace Game
{
    public sealed class PlayerStats : BaseObjectScene, ICollision
    {
        #region PlayerStats
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _coldResistance = 0.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _mentalResistance = 0.0f;
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float _physicalResistance = 0.0f;
        [SerializeField]
        [Range(100.0f, 400.0f)]
        private float _baseHealthPoint = 399.0f;
        private int _isMentalBurning;
        private bool _isDead = false;
        private float _damaged;
        public float _healthPoint;
        #endregion
        #region ICollision
        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (info.Source.tag == "Enemy")
            {
                if (_healthPoint > 0)
                {
                    var tempHp = _healthPoint;
                    _healthPoint -= info.Damage.ColdDamageValue * (1 - _coldResistance);
                    _healthPoint -= info.Damage.MentalDamageValue * (1 - _mentalResistance) * (1 + _isMentalBurning);
                    _healthPoint -= info.Damage.PhysicalDamageValue * (1 - _physicalResistance);
                    if (info.Damage.BulletEffect != 0)
                    {
                        GetEffect(info.Damage.BulletEffect);
                    }
                    _damaged = tempHp - _healthPoint;
                    WasDamaged(info, _damaged);
                }
                if (_healthPoint <= 0)
                {
                    BecomeDead();
                }                
            }
        }
        #endregion
        #region Methods
        protected override void Awake()
        {
            base.Awake();
            _baseHealthPoint = 300.0f;
            _healthPoint = _baseHealthPoint;
        }

        public void GetEffect(BulletEffects effect)
        {
            ///Получил эффект
            switch (effect)
            {
                case BulletEffects.Freeze:
                    _healthPoint -= 33 * (1 - _coldResistance); break;
                case BulletEffects.MentalBurn:
                    if (_isMentalBurning < 3) _isMentalBurning++; break;
                default:
                    break;
            }
        }

        private void BecomeDead()
        {
            _healthPoint = _baseHealthPoint;
            Debug.Log($"Игрок бессмертен! HP => MAX {_healthPoint}");
        }

        private void WasDamaged(InfoCollision info, float damage)
        {
            Debug.Log($"Игрок получил урон:{damage}, источник: {info.Source.name}");
        }
        #endregion
    }
}


