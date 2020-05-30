using UnityEngine;

namespace Game
{
	public sealed class Inventory : IInitialization
	{
		private BaseWeaponObject[] _weapons = new BaseWeaponObject[5];

		public BaseWeaponObject[] Weapons => _weapons;

		public FlashLightModel FlashLight { get; private set; }

		public void Initialization()
		{
			_weapons = ServiceLocatorMonoBehaviour.GetService<CharacterController>().
				GetComponentsInChildren<BaseWeaponObject>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
			FlashLight.Switch(FlashLightActiveType.Off);
		}
		
		public void RemoveWeapon(BaseWeaponObject weapon)
		{

		}
	}
}