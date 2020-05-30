using UnityEngine;
using System;

namespace Game
{
    public sealed class SelectorController : BaseController, IExecute
    {
        #region SelectorController
        private GameObject _dedicatedObj;
        private ISelectObj _selectedObj;
        private Vector2 _aim;
        private Camera _cameraMain;
        private bool _isSelectedObj;
        private bool _nullString;
        private string _uiText;

        public SelectorController()
        {
            _cameraMain = Camera.main;
            _aim = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
        }
        #endregion
        #region IExecute
        public void Execute()
        {
            //if (!IsActive) return;
            if (Physics.Raycast(_cameraMain.ScreenPointToRay(_aim), out var hit))
            {
                CheckHit(hit.collider.gameObject);
                _nullString = false;
            }
            else if (!_nullString)
            {
                _uiText = String.Empty;
                _nullString = true;
                _dedicatedObj = null;
                _isSelectedObj = false;
            }
            UiInterface.SelectorUi.Text = _uiText;
        }
        #endregion
        #region Methods
        public void CheckHit(GameObject obj)
        {
            /*
            if (obj == _dedicatedObj)
            {
                //_uiText = _selectedObj.GetMessage();
                return; 
            } */
            _selectedObj = obj.GetComponent<ISelectObj>();
            if (_selectedObj != null)
            {
                _uiText = _selectedObj.GetMessage();                
                _isSelectedObj = true;
            }
            else
            {
                _uiText = String.Empty;
                _isSelectedObj = false;
            }
            _dedicatedObj = obj;
        }
        #endregion
    }
}
