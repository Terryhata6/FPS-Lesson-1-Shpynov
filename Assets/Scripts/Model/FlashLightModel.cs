using UnityEngine;
using System;

namespace Game
{
    public sealed class FlashLightModel : BaseObjectScene
    {
        [SerializeField] private float _speed = 11;
        [SerializeField] private float _batteryChargeMax = 10;
        [SerializeField] private float _batteryChargeSpeedModifier = 4;
        private float _batteryChargeMaxReverse; 
        private Light _light;
        private Transform _goFollow;
        private Vector3 _vecOffset;
        public float BatteryChargeCurrent { get; private set; }
        

        protected override void Awake()
        {
            base.Awake();
            _batteryChargeMaxReverse = 1 / _batteryChargeMax;
            _light = GetComponent<Light>();
            _light.enabled = false;
            _goFollow = Camera.main.transform;
            _vecOffset = Transform.position - _goFollow.position;
            BatteryChargeCurrent = _batteryChargeMax;
        }

        public void Switch(FlashLightActiveType value)
        {
            switch (value)
            {
                case FlashLightActiveType.On:
                    _light.enabled = true;
                    Transform.position = _goFollow.position + _vecOffset;
                    Transform.rotation = _goFollow.rotation;
                    break;
                case FlashLightActiveType.Off:
                    _light.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        public void Rotation()
        {
            Transform.position = _goFollow.position + _vecOffset;
            Transform.rotation = Quaternion.Lerp(Transform.rotation,
                _goFollow.rotation, _speed * Time.deltaTime);
        }

        public float BatteryChargeBar() => BatteryChargeCurrent* _batteryChargeMaxReverse;
        
        /// <summary>
        /// Меняет заряд батареи
        /// </summary>
        /// <param name="value"></param>
        public void BatteryChargeEdit(BatteryEditType value)
        {
            switch (value)
            {
                case BatteryEditType.Charge:
                    if (BatteryChargeCurrent < _batteryChargeMax)
                        BatteryChargeCurrent += Time.deltaTime * _batteryChargeSpeedModifier;
                    break;
                case BatteryEditType.Discharge:
                    if (BatteryChargeCurrent > 0)
                        BatteryChargeCurrent -= Time.deltaTime ;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
            return;
        }
        /// <summary>
        /// Возвращает true если батарея пуста, иначе false;
        /// </summary>
        /// <returns></returns>
        public bool BatteryIsEmpty()
        {
            if (BatteryChargeCurrent <= 0)
            {
                BatteryChargeCurrent = 0;
                return true;
            }            
             return false;
        } 

        
    }
}