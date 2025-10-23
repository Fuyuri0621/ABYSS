using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    public float time;
       TextMeshProUGUI timerUI;


    private void Start()
    {

        timerUI = GetComponentInChildren<TextMeshProUGUI>();

    }
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            timerUI.text = time.ToString("00");
        }
        else if(time<0) { timerUI.text = "00"; time = 0;GameManager.Instance.Gameover(); }
    }
}
