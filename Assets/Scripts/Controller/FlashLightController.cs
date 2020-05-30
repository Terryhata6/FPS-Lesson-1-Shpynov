using UnityEngine;

namespace Game
{
    public sealed class FlashLightController : BaseController, IExecute, IInitialization
    {
        #region FlashLightController
        private FlashLightModel _flashLightModel;
        #endregion
        #region IInitialization
        public void Initialization()
        {
            _flashLightModel = Object.FindObjectOfType<FlashLightModel>();            
        }
        #endregion
        #region Methods
        /// <summary>
        /// Turn on flashlight
        /// </summary>
        public override void On()
        {
            if (IsActive) return;
            if (_flashLightModel.BatteryChargeCurrent <= 0) return;
            base.On();
            _flashLightModel.Switch(FlashLightActiveType.On);            
        }
        /// <summary>
        /// Turn off flashlight
        /// </summary>
        public override void Off()
        {
            if (!IsActive) return;
            base.Off();
            _flashLightModel.Switch(FlashLightActiveType.Off);            
        }
        #endregion
        #region IExecute        
        public void Execute()
        {
            UiInterface.FlashLightUiText.Text = _flashLightModel.BatteryChargeCurrent;
            UiInterface.FlashLightUiBar.SetBar = _flashLightModel.BatteryChargeBar();
            if (!IsActive)
            {
                _flashLightModel.BatteryChargeEdit(BatteryEditType.Charge);                
                return;
            }
            _flashLightModel.Rotation();
            _flashLightModel.BatteryChargeEdit(BatteryEditType.Discharge);
            if (_flashLightModel.BatteryIsEmpty()) Off();
        }
        #endregion
    }
}