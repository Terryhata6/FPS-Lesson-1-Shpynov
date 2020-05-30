using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class WeaponReloadUi : MonoBehaviour
    {
        private Image _reloadImage;
        private Text _reloadText;
        
        private void Awake()
        {
            _reloadImage = GetComponent<Image>();
        }

        public float ReloadBar
        {
            set => _reloadImage.fillAmount = value;
        }

        public void SetActive(bool value)
        {
            _reloadImage.gameObject.SetActive(value);
        }
    }
}


