using UnityEngine;

namespace Game
{
    public sealed class Controllers : IInitialization
    {
        #region Controllers
        private readonly IExecute[] _executeControllers;
        public int Length => _executeControllers.Length;
        public IExecute this[int index] => _executeControllers[index];

        public Controllers()
        {
            IMotor _motor = default;
            _motor = new UnitMotor(ServiceLocatorMonoBehaviour.GetService<CharacterController>());
            ServiceLocator.SetService(new TimeRemainingController());
            ServiceLocator.SetService(new Inventory());
            ServiceLocator.SetService(new PlayerController(_motor));
            ServiceLocator.SetService(new FlashLightController());
            ServiceLocator.SetService(new WeaponController());
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SelectorController());
            ServiceLocator.SetService(new BulletController());
            ServiceLocator.SetService(new SaveDataRepository());
            ServiceLocator.SetService(new LevelMakerController());
            ServiceLocator.SetService(new SmartEnemyesController());
            _executeControllers = new IExecute[8];

            _executeControllers[0] = ServiceLocator.Resolve<TimeRemainingController>();

            _executeControllers[1] = ServiceLocator.Resolve<PlayerController>();

            _executeControllers[2] = ServiceLocator.Resolve<FlashLightController>();

            _executeControllers[3] = ServiceLocator.Resolve<InputController>();

            _executeControllers[4] = ServiceLocator.Resolve<SelectorController>();

            _executeControllers[5] = ServiceLocator.Resolve<BulletController>();

            _executeControllers[6] = ServiceLocator.Resolve<WeaponController>();

            _executeControllers[7] = ServiceLocator.Resolve<SmartEnemyesController>();
        }
        #endregion
        #region IInitialization
        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }
            ServiceLocator.Resolve<LevelMakerController>().Initialization();
            ServiceLocator.Resolve<Inventory>().Initialization();
            ServiceLocator.Resolve<InputController>().On();                        
            ServiceLocator.Resolve<PlayerController>().On();
        }
        #endregion
    }
}
