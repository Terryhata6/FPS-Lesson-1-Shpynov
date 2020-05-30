using UnityEngine;

namespace Game
{
    public sealed class UiInterface
    {
        #region FlashLightUI
        private FlashLightUiText _flashLightUiText;

        public FlashLightUiText FlashLightUiText
        {
            get
            {
                if (!_flashLightUiText)
                    _flashLightUiText = Object.FindObjectOfType<FlashLightUiText>();
                return _flashLightUiText;
            }
        }

        private FlashLightUiBar _flashLightUiBar;
        public FlashLightUiBar FlashLightUiBar
        {
            get
            {
                if (!_flashLightUiBar)
                    _flashLightUiBar = Object.FindObjectOfType<FlashLightUiBar>();
                return _flashLightUiBar;
            }
        }
        #endregion
        #region SelectorUI
        private SelectorUi _selectorUi;
        public SelectorUi SelectorUi
        {
            get
            {
                if (!_selectorUi)
                    _selectorUi = Object.FindObjectOfType<SelectorUi>();
                return _selectorUi;
            }
        }
        #endregion
        #region WeaponUI
        private WeaponReloadUi _weaponReloadUi;
        public WeaponReloadUi WeaponReloadUi
        {
            get
            {
                if (!_weaponReloadUi)
                    _weaponReloadUi = Object.FindObjectOfType<WeaponReloadUi>();
                return _weaponReloadUi;
            }
        }

        private WeaponUi _weaponUi;
        public WeaponUi WeaponUi
        {
            get
            {
                if (!_weaponUi)
                    _weaponUi = Object.FindObjectOfType<WeaponUi>();
                return _weaponUi;
            }
        }
        #endregion
    }

}

