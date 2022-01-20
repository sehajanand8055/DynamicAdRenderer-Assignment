using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdComponents
{
    public class AdFrame : MonoBehaviour, IAdComponent
    {
        public RawImage frame;
        private RectTransform _rect;

        private void OnEnable()
        {
            _rect = frame.gameObject.GetComponent<RectTransform>();
        }

        public void SetFrameTexture(Texture texture)
        {
            frame.texture = texture;
        }


        public void SetSize(Vector2 size)
        {
            _rect.sizeDelta = size;
        }

        public void SetPosition(Vector2 pos)
        {
            _rect.position = frame.transform.TransformPoint(pos);
        }

        public void SetColor(Color color)
        {
            frame.color = color;
        }
    }
}