using UnityEngine;
using UnityEngine.UI;

public class UiCanvasScrene : MonoBehaviour
{
    private Text timerText;
    private Text personalHighscoreText;
    private TimerService timerService;
    private GameEngineService gameEngineService;
    // Start is called before the first frame update
    void Start()
    {
        timerService = FindObjectOfType<TimerService>();
        gameEngineService = FindObjectOfType<GameEngineService>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        personalHighscoreText = GameObject.Find("PersonalBestText").GetComponent<Text>();
        initialize(timerService, gameEngineService, timerText, personalHighscoreText);
    }

    // Update is called once per frame
    void Update()
    {
        refreshTimerText();
    }

    public void initialize(
        TimerService resolvedTimerService,
        GameEngineService resolvedGameEngineService,
        Text resolvedTimerText,
        Text resolvedPersonalHighscoreText)
    {
        timerService = resolvedTimerService;
        gameEngineService = resolvedGameEngineService;
        timerText = resolvedTimerText;
        personalHighscoreText = resolvedPersonalHighscoreText;
        timerService.DisplayTime(personalHighscoreText, gameEngineService.getPersonalHighscore());
    }

    public void refreshTimerText()
    {
        if (timerService.isTimeEnabled) {
            timerService.DisplayTime(timerText, timerService.currentTime);
        }
    }
}
