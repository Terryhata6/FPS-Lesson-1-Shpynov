using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class FlashLightUiBar : MonoBehaviour
    {
        private Image _barImage;
        private void Awake()
        {
            _barImage = GetComponent<Image>();            
        }

        public float SetBar
        {
            set => _barImage.fillAmount = value;
        }

        public void SetActive(bool value)
        {
            //_barImage.gameObject.SetActive(value);
        }
    }
}