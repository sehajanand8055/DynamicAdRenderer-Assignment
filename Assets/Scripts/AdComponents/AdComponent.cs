using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdComponents
{
    public interface IAdComponent
    {
        void SetSize(Vector2 size);
        void SetPosition(Vector2 pos);
        void SetColor(Color color);
    }
}