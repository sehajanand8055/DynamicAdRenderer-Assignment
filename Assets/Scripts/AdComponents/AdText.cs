using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdComponents
{
    public class AdText : MonoBehaviour,IAdComponent
    {
        public Text text;
        private RectTransform _rect;

        private void OnEnable()
        {
            _rect = text.gameObject.GetComponent<RectTransform>();
        }

        public void SetSize(Vector2 size)
        {
            _rect.sizeDelta = size;
        }

        public void SetPosition(Vector2 pos)
        {
            _rect.position = text.transform.TransformPoint(pos); 
        }

        public void SetColor(Color color)
        {
            text.color = color;
        }
        
        public void SetText(string value)
        {
            text.text = value;
        }
    }

}

