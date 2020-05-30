using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public sealed class SelectorUi : MonoBehaviour
    {
        private Text _selectionText;
        private void Awake()
        {
            _selectionText = GetComponent<Text>();
        }

        public string Text
        {
            set => _selectionText.text = value;
        }
    }
}