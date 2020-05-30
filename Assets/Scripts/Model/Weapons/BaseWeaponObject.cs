using UnityEngine;


namespace Game
{
    public abstract class BaseWeaponObject : BaseObjectScene
    {
        #region BaseWeaponObject
        public BaseAmmuObject Ammunition;
        public Clip Clip; //Запас патронов и объем обоймы
        
        public AmmunitionType[] AmmunitionTypes =
        { 
            AmmunitionType.Bullet
        };

        [SerializeField] protected Transform _barrel; //Ссылка на позицию источника выстрела
        [Range (600,1200)]
        [SerializeField] protected float _force = 999.0f; //Сила запущенного снаряда
        [SerializeField] protected float _rechargeTime = 0.2f; //Время между выстрелами 
        [SerializeField] protected float _reloadTime = 2.0f; //Время перезарядки
        [SerializeField] protected int _startClip = 180;
        [SerializeField] protected int _clipVolume = 30;
        [SerializeField] protected bool _unlimitedCharge = false;
        protected Transform _weaponOwner;
        protected bool _isReady = true;
        protected ITimeRemaining _timeRemaining;
        protected ITimeRemaining _reloadTimeRemaining;
        #endregion
        #region Methods
        private void Start()
        {
            _weaponOwner = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
            Debug.Log($"У оружия {name} владелец {_weaponOwner.tag} ");
            _timeRemaining = new TimeRemaining(ReadyShoot, _rechargeTime);
            _reloadTimeRemaining = new TimeRemaining(ReloadClip, _reloadTime);
            if(!_unlimitedCharge) Clip.TotalAmmunition = _startClip;            
            else Clip.TotalAmmunition = 999;
            Clip.VolumeAmmunition = _clipVolume;
            Clip.CountAmmunition = 0;
            ReloadClip();
        }


        /// <summary>
        /// Выстрел
        /// </summary>
        public abstract void Fire();


        /// <summary>
        /// Готовность стрелять
        /// </summary>
        protected void ReadyShoot()
        {
            _isReady = true;
        }

        /// <summary>
        /// Начинает перезарядку, выключает возможность стрельбы во время перезарядки 
        /// </summary>
        public void StartReloadClip()
        {
            if (Clip.TotalAmmunition > 0)
            {
                _isReady = false;
                _reloadTimeRemaining.AddTimeRemaining();
            }            
        }
        
        /// <summary>
        /// Перезаряжает обойму новыми снарядами
        /// </summary>
        private void ReloadClip()
        {
            if (!_unlimitedCharge)
            {
                if (Clip.TotalAmmunition < 1) return;                   //при пустом общем запасе не можем зарядиться  #todo вывод звука 
                Clip.TotalAmmunition += Clip.CountAmmunition;           //возвращаем запас из обоймы обратно в тотал
                if (Clip.TotalAmmunition >= Clip.VolumeAmmunition)      //если хватает на полную перезарядку, полностью перезаряжаем
                {
                    Clip.CountAmmunition = Clip.VolumeAmmunition;
                    Clip.TotalAmmunition -= Clip.VolumeAmmunition;
                }
                else                                                    //загружаем остаток 
                {
                    Clip.CountAmmunition = Clip.TotalAmmunition;
                }
            }
            else
            {
                Clip.CountAmmunition = Clip.VolumeAmmunition;
            }
            _isReady = true;
        }

        public void AddAmmo(int value)
        {
            if (!_unlimitedCharge)
                Clip.TotalAmmunition += value;
        }
        #endregion
    }
}
