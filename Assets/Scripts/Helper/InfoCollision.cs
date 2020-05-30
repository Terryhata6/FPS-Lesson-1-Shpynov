using UnityEngine;

namespace Game
{
    public readonly struct InfoCollision
    {
        private readonly Vector3 _dir;
        private readonly DamageCapsule _damage;
        private readonly Transform _source;

        public InfoCollision(DamageCapsule damage, Transform source, Vector3 dir = default)
        {
            _damage = damage;
            _source = source;
            _dir = dir;
        }

        public Vector3 Dir => _dir;

        public DamageCapsule Damage => _damage;

        public Transform Source => _source;

        
    }
}
