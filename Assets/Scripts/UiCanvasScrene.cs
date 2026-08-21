using UnityEngine;
using UnityEngine.UI;

public class UiCanvasScrene : MonoBehaviour
{
    private Text timerText;
    private TimerService timerService;

    // Start is called before the first frame update
    void Start()
    {
        timerService = Services.Timer;
        timerText = UiText.Find("TimerText");
        UiText.DisplayTime(UiText.Find("PersonalBestText"), Services.GameEngine.getPersonalHighscore());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerService.isTimeEnabled) {
            UiText.DisplayTime(timerText, timerService.currentTime);
        }
    }
}
