namespace Game
{
    public sealed class MentalBurnerWeaponModel : BaseWeaponObject
    {

        protected override void Awake()
        {
            base.Awake();
            _rechargeTime = 1.0f;
            _clipVolume = 5;
            _unlimitedCharge = true;
        }

        public override void Fire()
        {
            if (!_isReady) return;
            if (Clip.CountAmmunition <= 0)
            {
                StartReloadClip();
                return;
            } 
            var temAmmunition = Instantiate(Ammunition, _barrel.position, _barrel.rotation);//todo Pool object
            temAmmunition.AddForce(_barrel.forward * _force);
            temAmmunition._source = _weaponOwner;
            Clip.CountAmmunition--;
            _isReady = false;
            _timeRemaining.AddTimeRemaining();
        }
    }
}
