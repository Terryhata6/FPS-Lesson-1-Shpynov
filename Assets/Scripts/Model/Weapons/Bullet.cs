using UnityEngine;

namespace Game
{
    public class Bullet : BaseAmmuObject
    {
        private void OnCollisionEnter(Collision collision)
        { 
            var setDamage = collision.gameObject.GetComponent<ICollision>();
            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_projectileDamage, _source, Rigidbody.velocity));
            }
            DestroyAmmunition();
        }
    }
}

