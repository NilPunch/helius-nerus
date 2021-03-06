﻿using UnityEngine;

namespace HNUI
{
    public class SensitivitySlider : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider _slider = null;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("Sensitivity"))
                _slider.value = PlayerPrefs.GetFloat("Sensitivity");
        }


        public void SliderValueChanged(float sens)
        {
			if (Mathf.Abs(sens - 1f) < 0.03)
			{
				sens = 1.0f;
				_slider.value = 1.0f;
			}
            PlayerPrefs.SetFloat("Sensitivity", sens);
        }
    }
}



