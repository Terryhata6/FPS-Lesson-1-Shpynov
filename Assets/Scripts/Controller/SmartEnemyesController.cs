using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public sealed class SmartEnemyesController : BaseController, IExecute, IInitialization
    {
        private List<SmartEnemyObject> _smartEnemyes= new List<SmartEnemyObject>();
        //private SmartEnemyObject _smartEnemyes2= new SmartEnemyObject();
        //private Transform _playerTransform;
        public void Initialization() 
        {
            //_playerTransform = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
            _smartEnemyes.AddRange(Object.FindObjectsOfType<SmartEnemyObject>());
            //_smartEnemyes2 = Object.FindObjectOfType<SmartEnemyObject>();
        }
        
        public void Execute()
        {
            foreach (var enemy in _smartEnemyes)
            {
                enemy.MoveSet();
            }
            //_smartEnemyes2.MoveSet(_playerTransform);
        }
        
        public void AddBotToList(SmartEnemyObject obj) 
        {
            _smartEnemyes.Add(obj);
        }
    }
}
