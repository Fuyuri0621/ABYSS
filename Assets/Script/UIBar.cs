using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    [Serializable]
    public class StatusBarGroup
    {
        public string name = string.Empty;
        public Slider slider;
    }
    public class UIBar : MonoBehaviour
    {
        [SerializeField] List<StatusBarGroup> BarGroup = new List<StatusBarGroup>();
        public void UpdateSlider(string slidername, int current, int max)
        {
            StatusBarGroup traget = BarGroup.Find(s => s.name == slidername);
            traget.slider.maxValue = max;    
            traget.slider.value = current;

        }

    }

