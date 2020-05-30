using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class WeaponUi : MonoBehaviour
    {
        private Text _ammoText;       

        private void Awake()
        {
            _ammoText = GetComponent<Text>();            
        }

        public string ShowData
        {
            set => _ammoText.text = value;
        }

        public void SetActive(bool value)
        {
            _ammoText.gameObject.SetActive(value);
        }

        public bool IsActive() { return _ammoText.gameObject.activeSelf; }
    }
}
