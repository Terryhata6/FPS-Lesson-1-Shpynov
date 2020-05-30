

namespace Game
{
    public sealed class M4A1Model : BaseWeaponObject
    {
        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0) return;
            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);//todo Pool object
            temAmmunition.AddForce(_barrel.forward * _force);
            temAmmunition._source = _weaponOwner;
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}