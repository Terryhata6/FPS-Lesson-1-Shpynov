using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private Controllers _controllers;
        void Start()
        {
            _controllers = new Controllers();
            _controllers.Initialization();
        }
        void Update()
        {
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }
    }
}