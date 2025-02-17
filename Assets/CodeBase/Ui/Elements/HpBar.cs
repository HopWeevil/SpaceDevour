﻿using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image ImageCurrent;

        public void SetValue(float current, float max)
        {
            ImageCurrent.fillAmount = current / max;
        }
    }
}
