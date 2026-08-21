using UnityEngine;
using UnityEngine.UI;

public class UiCanvasScrene : MonoBehaviour
{
    private Text timerText;
    private TimerService timerService;

    // Start is called before the first frame update
    void Start()
    {
        timerService = Services.Require<TimerService>(this, "the timer UI stays disabled");
        GameEngineService gameEngineService = Services.Require<GameEngineService>(this, "the timer UI stays disabled");
        timerText = UiText.Find("TimerText", this);
        Text personalHighscoreText = UiText.Find("PersonalBestText", this);

        if (!timerService || !gameEngineService || !timerText || !personalHighscoreText) {
            Debug.LogError("Missing dependencies, the timer UI stays disabled.", this);
            enabled = false;
            return;
        }

        UiText.DisplayTime(personalHighscoreText, gameEngineService.getPersonalHighscore());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerService.isTimeEnabled) {
            UiText.DisplayTime(timerText, timerService.currentTime);
        }
    }
}
