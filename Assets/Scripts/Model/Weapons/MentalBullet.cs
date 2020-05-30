using UnityEngine;

namespace Game
{
    public class MentalBullet : BaseAmmuObject
    {
        //bool _scaling;  TODO Исправить контроллер снарядов
        protected override void Awake()
        {
            base.Awake();
            //_scaling = true;
            Type = AmmunitionType.ScalebleMentalBullet;
            _projectileDamage.BulletEffect = BulletEffects.MentalBurn;

        }

        private void OnCollisionEnter(Collision collision)
        {
            var setDamage = collision.gameObject.GetComponent<ICollision>();
            if (setDamage != null)
            {
                setDamage.CollisionEnter(new InfoCollision(_projectileDamage, _source, Rigidbody.velocity));
            }
            //_scaling = false;
            DestroyAmmunition();
        }
    }
}

