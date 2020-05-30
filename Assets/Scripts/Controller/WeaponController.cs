namespace Game
{
    public sealed class WeaponController : BaseController, IExecute
    {
        #region WeaponController
        private BaseWeaponObject _weapon;
        #endregion
        #region Methods
        public override void On(params BaseObjectScene[] weapon)
        {
            if (IsActive) return;
            if (weapon.Length > 0) _weapon = weapon[0] as BaseWeaponObject;
            if (_weapon == null) return;
            base.On(_weapon);
            _weapon.IsVisible = true;
            UiInterface.WeaponUi.SetActive(true);
        }

        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _weapon.IsVisible = false;
            _weapon = null;
            UiInterface.WeaponUi.SetActive(false);
        }

        public void Fire()
        {
            _weapon.Fire();
        }

        public void ReloadClip()
        {
            _weapon.StartReloadClip();
        }

        /// <summary>
        /// Добавляет патроны к оружию
        /// </summary>
        /// <param Количество патронов="value"></param>
        public void AddAmmo(int value)
        {
            _weapon.AddAmmo(value);
        }
        #endregion
        #region IExecute
        public void Execute()
        {
            if (IsActive)
            {
                UiInterface.WeaponUi.ShowData = $"{_weapon.Clip.CountAmmunition}/{_weapon.Clip.TotalAmmunition}";
            }
        }
        #endregion
    }
}
