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
        timerText = FindText("TimerText");
        personalHighscoreText = FindText("PersonalBestText");

        if (!timerService || !gameEngineService || !timerText || !personalHighscoreText) {
            Debug.LogError("Missing dependencies, the timer UI stays disabled.", this);
            enabled = false;
            return;
        }

        timerService.DisplayTime(personalHighscoreText, gameEngineService.getPersonalHighscore());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerService.isTimeEnabled) {
            timerService.DisplayTime(timerText, timerService.currentTime);
        }
    }

    private Text FindText(string objectName)
    {
        GameObject target = GameObject.Find(objectName);
        if (!target) {
            Debug.LogError($"GameObject '{objectName}' not found in the scene.", this);
            return null;
        }

        Text text = target.GetComponent<Text>();
        if (!text) {
            Debug.LogError($"GameObject '{objectName}' has no {nameof(Text)} component.", target);
        }
        return text;
    }
}
