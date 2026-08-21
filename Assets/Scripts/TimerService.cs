using UnityEngine;


public class TimerService : MonoBehaviour
{
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
        return GameStorage.FinalTime;
    }

    public void setFinalTime(float time)
    {
        GameStorage.FinalTime = time;
    }
}
