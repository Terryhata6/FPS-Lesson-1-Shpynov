using UnityEngine;

namespace Game
{
    public sealed class BulletController : BaseController, IExecute
    {
        #region BulletController
        private BaseAmmuObject _bullet;
        private Vector3 _scale;
        private int _scaleCounter = 0;
        #endregion
        #region IExecute        
        public void Execute()
        {            
            if (_scaleCounter < 3)
                _scaleCounter++;
            else
            {
                _bullet = Object.FindObjectOfType<MentalBullet>();
                if (_bullet != null)
                {
                    _scaleCounter = 0;
                    if (_bullet.Type == AmmunitionType.ScalebleMentalBullet)
                    {
                        _scale = _bullet.Transform.localScale;
                        _bullet.Transform.localScale = new Vector3(_scale.x * 1.1f, _scale.y * 1.1f, _scale.z * 1.1f);
                    }
                }
            }                   
        }
        #endregion
    }
}
