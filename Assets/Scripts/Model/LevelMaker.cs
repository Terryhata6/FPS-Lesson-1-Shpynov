//using System.Numerics;
using System.Drawing;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Game
{
    public sealed class LevelMaker : MonoBehaviour
    {
        #region LevelMaker
        [SerializeField]
        public GameObject _room;
        [SerializeField]
        public SmartEnemyObject _enemy;
        [SerializeField]
        public GameObject _bridgeLR;
        [SerializeField]
        public GameObject _bridgeUD;
        [SerializeField]
        [Range(6.0f, 30.0f)]
        public int _width = 12;
        [SerializeField]
        [Range(6.0f, 30.0f)]
        public int _height = 12;
        #endregion
        #region Methods
        public void CreateBridge(int x, int z, CellTypes bridgeType)
        {
            if (bridgeType == CellTypes.BridgeLR)
                Instantiate(_bridgeLR, new Vector3((x - 2) * 20, 0, (z - 2) * 20), new Quaternion());
            else if (bridgeType == CellTypes.BridgeUD)
                Instantiate(_bridgeUD, new Vector3((x - 2) * 20, 0, (z - 2) * 20), new Quaternion());
        }

        public void CreateRoom(int x, int z)
        {
            Vector3 _roomPosition = new Vector3((x - 2) * 20, 0, (z - 2) * 20);
            Vector3 _enemyPosition = _roomPosition + new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));
            Instantiate(_room, _roomPosition, new Quaternion());            
        }

        public void AddEnemyes(int x, int z)
        {
            float pointX = ((x - 2) * 20) + Random.Range(-5.0f, 5.0f);
            float pointY = 1.0f;
            float pointZ = ((z - 2) * 20) + Random.Range(-5.0f, 5.0f);
            Vector3 _enemyPosition = new Vector3(pointX, pointY, pointZ);
            var tempEnemy = Instantiate(_enemy, _enemyPosition, new Quaternion());
            tempEnemy.PlayerTransform = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
            ServiceLocator.Resolve<SmartEnemyesController>().AddBotToList(tempEnemy);
        }
        #endregion
    }
}
