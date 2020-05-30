using UnityEngine;

namespace Game
{
    public abstract class BaseAmmuObject : BaseObjectScene
    {
        [SerializeField] 
        private float _timeToDestruct = 10.0f;
        [SerializeField] 
        private float _physicalDamage = 10.0f;
        [SerializeField]
        private float _coldDamage = 0.0f;
        [SerializeField] 
        private float _mentalDamage = 0.0f;
        [Range(1.0f, 3.0f)]
        [SerializeField]
        //private float _destroyOnCollisionTime = 2.0f;
        //private ITimeRemaining _destroyOnCollisionTimeRemaining;               
        private ITimeRemaining _timeRemaining;
        protected DamageCapsule _projectileDamage; 
        public AmmunitionType Type = AmmunitionType.Bullet;
        public Transform _source;
        
        protected override void Awake()
        {
            base.Awake();
            _projectileDamage.PhysicalDamageValue = _physicalDamage;
            _projectileDamage.MentalDamageValue = _mentalDamage;
            _projectileDamage.ColdDamageValue = _coldDamage;
            _projectileDamage.BulletEffect = 0;
        }

        private void Start()
        {
            Destroy(gameObject, _timeToDestruct);            
        }

        public void AddForce(Vector3 dir)
        {
            if (!Rigidbody) return;
            Rigidbody.AddForce(dir);
        }

        protected void DestroyAmmunition() 
        {
            DestroyAmmunition(0.0f);
        }
        protected void DestroyAmmunition(float destoyTime)
        {
            Destroy(gameObject, destoyTime);
            //_timeRemaining.RemoveTimeRemaining();            
        }
    }
}
