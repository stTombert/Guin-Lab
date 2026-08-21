using UnityEngine;
using UnityEngine.UI;

public class UiSuccess : MonoBehaviour
{
    private Text currentTimeText;
    private Text highscoreText;
    private TimerService timerService;
    private GameEngineService gameEngineService;

    // Start is called before the first frame update
    void Start()
    {
        timerService = FindObjectOfType<TimerService>();
        gameEngineService = FindObjectOfType<GameEngineService>();
        currentTimeText = GetComponent<Text>();
        highscoreText = GameObject.Find("HighscoreText").GetComponent<Text>();
        initialize(timerService, gameEngineService, currentTimeText, highscoreText);
    }

    public void initialize(
        TimerService resolvedTimerService,
        GameEngineService resolvedGameEngineService,
        Text resolvedCurrentTimeText,
        Text resolvedHighscoreText)
    {
        timerService = resolvedTimerService;
        gameEngineService = resolvedGameEngineService;
        currentTimeText = resolvedCurrentTimeText;
        highscoreText = resolvedHighscoreText;
        timerService.DisplayTime(currentTimeText, timerService.getFinalTime());
        timerService.DisplayTime(highscoreText, gameEngineService.getPersonalHighscore());
    }
}
