
using UnityEngine;
using UnityEngine.AI;

namespace Game 
{
    public sealed class SmartEnemyObject : BaseEnemyObject
    {
        #region SmartEnemyObject
        private NavMeshAgent _agent;
        public float _activeDis = 20.0f;
        private float _currentDis = 999.0f;
        private float _meleeActiveDis = 2.0f;
        [SerializeField]
        private Conditions _objectCondition;        
        private float _updateConditonTime = 10.0f;
        [SerializeField]
        [Range(3.0f, 30.0f)]
        private float _regenerationRaiting = 3.0f;
        [SerializeField]
        private bool _selfRegeneration = true;
        [SerializeField]
        private Transform _enemyBarrel;
        [SerializeField]
        private Bullet _enemyBullet;

        private bool _isEvades = false;
        private ITimeRemaining _updateConditionTimeRemaining;
        private ITimeRemaining _inviseOnDeathTimeRemaining;
        private ITimeRemaining _chillTimeRemaining;
        private ITimeRemaining _rangeAttackTimeRemaining;
        private ITimeRemaining _meleeAttackTimeRemaining;
        private float _rangeAttackCooldownTime = 3.0f;
        private float _meleeAttackCooldownTime = 0.5f;
        private bool _playerIsVisible = false;
        private bool _rangeAttackIsReady = true;
        public Transform PlayerTransform { get; set; }
        public Vision _vision;
        private bool _meleeAttackIsReady = true;
        #endregion
        #region Methods
        protected override void Awake()
        {
            base.Awake();      
            _updateConditionTimeRemaining = new TimeRemaining(() => UpdateCondition(), _updateConditonTime, true);
            _inviseOnDeathTimeRemaining = new TimeRemaining(() => InviseOnDeath(), 3.0f);
            _rangeAttackTimeRemaining = new TimeRemaining(() => RangeAttackSetReady(), _rangeAttackCooldownTime);
            _meleeAttackTimeRemaining = new TimeRemaining(() => MeleeAttackSetReady(), _meleeAttackCooldownTime);            
            _chillTimeRemaining = new TimeRemaining(() => StartPatrol(), 3.0f);
            _vision = new Vision();
            _agent = GetComponent<NavMeshAgent>();            
            SetCondition(Conditions.None);
            _updateConditionTimeRemaining.AddTimeRemaining();   
            
        }
        /// <summary>
        /// Set dead condition to object
        /// </summary>
        protected override void BecomeDead()
        {
            base.BecomeDead();
            _agent.enabled = false;
            _inviseOnDeathTimeRemaining.AddTimeRemaining();
            SetCondition(Conditions.Dead);
        }
        /// <summary>
        /// Actions when was damaged
        /// </summary>
        /// <param name="info"></param>
        protected override void WasDamaged(InfoCollision info)
        {
            base.WasDamaged(info);
            SetCondition(Conditions.Angry);
        }

        /// <summary>
        /// Spawn object on base position
        /// </summary>
        public override void Spawn() 
        {
            base.Spawn();
            _agent.enabled = true;
            SetCondition(Conditions.None);
        }

        /// <summary>
        /// Update Method
        /// </summary>
        /// <param name="PlayerTransform"></param>
        public void MoveSet()
        {
            if (_isDead)
                SetCondition(Conditions.Dead);
            if (_objectCondition != Conditions.Dead) 
            {
                if (!_isEvades)
                {
                    if (_vision.VisionM(Transform, PlayerTransform, _activeDis, out _currentDis))//Определяем видимость игрока объектом
                    {
                        _playerIsVisible = true;
                        _objectCondition = Conditions.Angry;
                    }
                    else
                        _playerIsVisible = false;
                }
                if (_isEvades)
                {
                    SelfRegeneration();
                    if (_healthPoint == _baseHealthPoint)
                        _isEvades = false;
                }
            }            
            switch (_objectCondition)
            {
                case Conditions.None:
                    if(_agent.hasPath)_agent.SetDestination(Transform.position);
                    SetCondition(Conditions.ChillAndStartPatrol);
                    break;
                case Conditions.ChillAndStartPatrol:
                    _chillTimeRemaining.AddTimeRemaining();//запускает метод StartPatrol
                    break;
                case Conditions.Patrol:
                    break;
                case Conditions.Angry:
                    if (_rangeAttackIsReady)
                    {
                        RangeAttack(PlayerTransform);
                    }                        
                    else if (_meleeAttackIsReady)
                    {
                        if(_currentDis <= _activeDis*_activeDis)
                            MeleeAttack(PlayerTransform);
                        SetCondition(Conditions.Chase);
                    }
                    else SetCondition(Conditions.Chase);
                    break;
                case Conditions.Chase:
                    if (_currentDis > _activeDis * _activeDis)
                    {
                        MoveTo(PlayerTransform.position);
                    }
                    else if (!_rangeAttackIsReady)
                    {
                        if (_currentDis > _meleeActiveDis * _meleeActiveDis)
                            MoveTo(PlayerTransform.position);
                        else if (_meleeAttackIsReady)
                            SetCondition(Conditions.Angry);
                    }
                    else 
                    {
                        SetCondition(Conditions.Angry);
                    } 
                    break;
                case Conditions.Dead:
                    break;
                default:
                    break;
            }            
        }
        /// <summary>
        /// Object's melee attack
        /// </summary>
        /// <param name="target"></param>
        private void MeleeAttack(Transform target)
        {
            //МИЛИАТАКВГЕРОЯ
            Debug.Log($"{Name} attack the player on Melee");
            _meleeAttackIsReady = false;
            _meleeAttackTimeRemaining.AddTimeRemaining();
        }
        /// <summary>
        /// Set melee attack ready
        /// </summary>
        private void MeleeAttackSetReady() 
        {
            _meleeAttackIsReady = true;
        }

        /// <summary>
        /// Object's range attack
        /// </summary>
        private void RangeAttack(Transform target)
        {            
            var obj = Instantiate(_enemyBullet, _enemyBarrel.position, _enemyBarrel.rotation);
            obj.AddForce((target.position - _enemyBarrel.position) * 60.0f);
            obj._source = transform;
            Debug.Log($"{Name} attack the player on Range");
            _rangeAttackIsReady = false;
            _rangeAttackTimeRemaining.AddTimeRemaining();
        }

        /// <summary>
        /// Set range attack ready
        /// </summary>
        private void RangeAttackSetReady()
        {
            _rangeAttackIsReady = true;
        }

        /// <summary>
        /// Regeneration Tick
        /// </summary>
        private void SelfRegeneration()
        {
            if(_selfRegeneration)
            {
                if (_healthPoint < _baseHealthPoint)
                    Heal(_regenerationRaiting * Time.deltaTime);
            }
        }

        /// <summary>
        /// Update object's condition
        /// </summary>
        private void UpdateCondition() 
        {
            if (_objectCondition == Conditions.Angry)
            {
                 if (!_playerIsVisible)
                 {
                    _isEvades = true;
                    _objectCondition = Conditions.None;
                 }
            }
            if (_objectCondition == Conditions.Patrol)
            {
                SetCondition(Conditions.None);
            }
        }
        
        /// <summary>
        /// Set condition Patrol and send agent to next point
        /// </summary>
        private void StartPatrol()
        {
            SetCondition(Conditions.Patrol);
            var _dis = Random.Range(5, 50);
            var _randomPoint = Random.insideUnitSphere * _dis;
            NavMesh.SamplePosition(Transform.position + _randomPoint,
                out var hit, _dis, NavMesh.AllAreas);
            Vector3 tempPosition = hit.position;
            MoveTo(tempPosition);
        }

        /// <summary>
        /// Send agent to point
        /// </summary>
        /// <param point="tempPosition"></param>
        private void MoveTo(Vector3 tempPosition)
        {
            _agent.SetDestination(tempPosition);
        }
        /// <summary>
        /// Set condition to value
        /// </summary>
        /// <param Condition="value"></param>
        private void SetCondition(Conditions value)
        {
            _objectCondition = value;
        }
        /// <summary>
        /// Get invisibility after death and before spawn 
        /// </summary>
        private void InviseOnDeath() 
        {
            if (_objectCondition == Conditions.Dead)
            { 
                IsVisible = false;
                Transform.position = _trashPosition;
            }            
        }
        #endregion
    }
}



