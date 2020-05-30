using UnityEngine;

namespace Game
{
	public sealed class Inventory : IInitialization
	{
		#region Inventory
		private BaseWeaponObject[] _weapons = new BaseWeaponObject[5];
		public BaseWeaponObject[] Weapons => _weapons;
		public FlashLightModel FlashLight { get; private set; }
		#endregion
		#region IInitialization
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
		#endregion
		#region Methods
		public void RemoveWeapon(BaseWeaponObject weapon)
		{

		}
		#endregion
	}
}