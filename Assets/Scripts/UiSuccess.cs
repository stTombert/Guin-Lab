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
            Debug.LogError($"{nameof(TimerService)} or {nameof(GameEngineService)} not found in the scene, the result times stay empty.", this);
            return;
        }

        currentTimeText = GetComponent<Text>();
        if (!currentTimeText) {
            Debug.LogError($"No {nameof(Text)} component on this GameObject, cannot show the final time.", this);
        } else {
            timerService.DisplayTime(currentTimeText, timerService.getFinalTime());
        }

        GameObject highscoreObject = GameObject.Find("HighscoreText");
        if (!highscoreObject) {
            Debug.LogError("GameObject 'HighscoreText' not found in the scene.", this);
            return;
        }

        highscoreText = highscoreObject.GetComponent<Text>();
        if (!highscoreText) {
            Debug.LogError($"GameObject 'HighscoreText' has no {nameof(Text)} component.", highscoreObject);
            return;
        }

        timerService.DisplayTime(highscoreText, gameEngineService.getPersonalHighscore());
    }
}
