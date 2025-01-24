using TMPro;
using UnityEngine;

namespace GGJ2025.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]  
    public class TextWrapper : MonoBehaviour  
    {
        private TextMeshProUGUI _text;
        protected virtual void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        
        protected void SetLineSpacing(float spacing)  
        {
            _text.lineSpacing = spacing;  
        }

        public void SetText(string text)  
        {
            _text.text = text;  
        }
        
        public void SetText(int text)  
        {
            _text.text = text.ToString();  
        }

        public void SetColor(Color color)
        {
            _text.color = color;
        }

        public void SetActive(bool isActive)  
        {
            _text.gameObject.SetActive(isActive);  
        }
    }
}