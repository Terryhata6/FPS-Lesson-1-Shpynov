using UnityEngine;

namespace Game
{ 
    public class Mine : BaseObjectScene
    {
        [SerializeField]
        private float _physicalDamage = 10.0f;
        [SerializeField]
        private float _coldDamage = 0.0f;
        [SerializeField]
        private float _mentalDamage = 0.0f;                    
        private ITimeRemaining _timeRemaining;
        protected DamageCapsule _mineDamage;
        public Transform _source;
        private bool _isReady;

        protected override void Awake()
        {
            base.Awake();
            _isReady = true;
            _mineDamage.PhysicalDamageValue = _physicalDamage;
            _mineDamage.MentalDamageValue = _mentalDamage;
            _mineDamage.ColdDamageValue = _coldDamage;
            _mineDamage.BulletEffect = 0;
            _timeRemaining = new TimeRemaining(() => Cooldown(), 3.0f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isReady) 
            {
                if (collision.gameObject.tag == "Player")
                {
                    var setDamage = collision.gameObject.GetComponent<ICollision>();
                    if (setDamage != null)
                    {
                        setDamage.CollisionEnter(new InfoCollision(_mineDamage, _source, Rigidbody.velocity));
                    }
                    _timeRemaining.AddTimeRemaining();
                }
            }                      
        }

        private void Cooldown() 
        {
            _isReady = true;
        }
    }
}