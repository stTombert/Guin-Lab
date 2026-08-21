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
        if (!timerService || !gameEngineService) {
            Debug.LogWarning("TimerService oder GameEngineService fehlt in der Szene");
            return;
        }

        currentTimeText = GetComponent<Text>();
        if (currentTimeText) {
            timerService.DisplayTime(currentTimeText, timerService.getFinalTime());
        }

        GameObject highscoreObject = GameObject.Find("HighscoreText");
        highscoreText = highscoreObject ? highscoreObject.GetComponent<Text>() : null;
        if (highscoreText) {
            timerService.DisplayTime(highscoreText, gameEngineService.getPersonalHighscore());
        }
    }
}
