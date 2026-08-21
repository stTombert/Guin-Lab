using UnityEngine;
using UnityEngine.UI;
using System;


public class TimerService : MonoBehaviour
{
    private const string FinalTimeKey = "finalTime";

    public float currentTime = 0;
    public bool isTimeEnabled = false;

    void Start()
    {
        startTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimeEnabled)
        {
            currentTime += Time.deltaTime;
        }
    }

    public void DisplayTime(Text target, float timeToDisplay, string format = "mm\\:ss\\.fff")
    {
        if (!target)
        {
            Debug.LogError("No text target given, cannot display the time.", this);
            return;
        }
        target.text = TimeSpan.FromSeconds(timeToDisplay).ToString(format);
    }

    public void startTime()
    {
        isTimeEnabled = true;
    }

    public void stopTime()
    {
        isTimeEnabled = false;
    }

    public float getFinalTime()
    {
        return PlayerPrefs.GetFloat(FinalTimeKey, 0);
    }

    public void setFinalTime(float time)
    {
        PlayerPrefs.SetFloat(FinalTimeKey, time);
        PlayerPrefs.Save();
    }
}
