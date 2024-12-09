using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{

    static public float time;
    [SerializeField] private TextMeshProUGUI text;



    // Start is called before the first frame update
    void Start()
    {
        showTime(time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        showTime(time);
    }

    private void showTime(float time)
    {
        time += 1;
        
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);


        text.text = String.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
