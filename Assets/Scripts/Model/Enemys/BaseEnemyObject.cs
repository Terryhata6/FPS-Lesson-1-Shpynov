using System;
using UnityEngine;

namespace Game
{
    public abstract class BaseEnemyObject : BaseObjectScene, ICollision, ISelectObj
    {
        #region BaseEnemyObject
        public event Action OnPointChange = delegate { };
        [SerializeField] protected float _baseHealthPoint = 100.0f;
        [SerializeField] protected float _basePhysicalResistance = 0.0f;
        [SerializeField] protected float _baseColdResistance = 0.0f;
        [SerializeField] protected float _baseMentalResistance = 0.0f;
        [SerializeField] protected bool _canRespawn = true;
        [SerializeField][Range(3.0f, 30.0f)] private float _customRespawnTime = 15.0f;
        protected float _healthPoint;
        protected float _physicalResistance;
        protected float _coldResistance;
        protected float _mentalResistance;        
        protected bool _isDead;
        private float _timeToDestroy = 10.0f;               
        //private string _hpToString;
        //private ITimeRemaining _effectTimeRemaining; //таймер наложенных состояний
        private int _isMentalBurning = 0;
        private ITimeRemaining _respawnTimeRemaining;
        protected Vector3 _trashPosition;
        public float Hp
        {
            get => _healthPoint;
            private set => _healthPoint = value;
        }
        #endregion
        #region ICollision
        public void CollisionEnter(InfoCollision info)
        {
            if (_isDead) return;
            if (info.Source.tag == "Player")
            {
                WasDamaged(info);
                if (Hp > 0)
                {
                    Hp -= info.Damage.ColdDamageValue * (1 - _coldResistance);
                    Hp -= info.Damage.MentalDamageValue * (1 - _mentalResistance) * (1 + _isMentalBurning);
                    Hp -= info.Damage.PhysicalDamageValue * (1 - _physicalResistance);
                    if (info.Damage.BulletEffect != 0)
                    {
                        GetEffect(info.Damage.BulletEffect);
                    }
                }
                if (Hp <= 0)
                {
                    BecomeDead();
                }
            }
        }
        #endregion
        #region ISelectObj
        /// <summary>
        /// Возвращает строку с информацией о выбранном объекте
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            return $"{gameObject.name} - {Hp:0}";
        }
        #endregion
        #region Methods
        protected override void Awake()
        {
            base.Awake();
            _trashPosition = new Vector3(-42.0f, -42.0f, -42.0f);
            _canRespawn = true;
            _respawnTimeRemaining = new TimeRemaining(() => Spawn(), _customRespawnTime);
            Refresh();            
        }

        public void Refresh()
        {
            _healthPoint = _baseHealthPoint;
            _physicalResistance = _basePhysicalResistance;
            _coldResistance = _baseColdResistance;
            _mentalResistance = _baseMentalResistance;
            _isMentalBurning = 0;
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="effect"></param>
        public void GetEffect(BulletEffects effect)
        {
            ///Получил эффект
            switch(effect)
            {
                case BulletEffects.Freeze: 
                    Hp -= 33 * (1 - _coldResistance); break;
                case BulletEffects.MentalBurn: 
                    if (_isMentalBurning < 3) _isMentalBurning++; break;
                default: 
                    break;
            }
        }
        
        protected virtual void WasDamaged(InfoCollision info) { }

        public virtual void Spawn() 
        {
            Spawn(BasePosition);
        }
        /// <summary>
        /// Спавнит объект в выбранной позиции
        /// </summary>
        /// <param name="_newSpawnPosition"></param>
        public virtual void Spawn(Vector3 _newSpawnPosition) 
        {
            
            DisableRigidBody();
            _isDead = false;
            IsVisible = true;
            Transform.position = _newSpawnPosition;
            Refresh();
        }
        /// <summary>
        /// Исцеляет
        /// </summary>
        /// <param name="value"></param>
        public void Heal(float value)
        {
            _healthPoint += value;//TODO добавить отображение исцеления в UI
            if (_healthPoint > _baseHealthPoint) _healthPoint = _baseHealthPoint;
        }

        protected virtual void BecomeDead() 
        {            
            if (!TryGetComponent<Rigidbody>(out _))
            {
                gameObject.AddComponent<Rigidbody>();
            }
            else EnableRigidBody();
            if (_canRespawn)
            {
                _respawnTimeRemaining.AddTimeRemaining();
            }
            else
            {                
                Destroy(gameObject, _timeToDestroy);
                OnPointChange.Invoke();                
            }
            _isDead = true;
        }
        #endregion
        
    }
}
