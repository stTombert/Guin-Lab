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

        if (timerService && gameEngineService && personalHighscoreText) {
            timerService.DisplayTime(personalHighscoreText, gameEngineService.getPersonalHighscore());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timerService && timerText && timerService.isTimeEnabled) {
            timerService.DisplayTime(timerText, timerService.currentTime);
        }
    }

    Text FindText(string objectName)
    {
        GameObject target = GameObject.Find(objectName);
        if (!target) {
            Debug.LogWarning("GameObject " + objectName + " nicht gefunden");
            return null;
        }

        Text text = target.GetComponent<Text>();
        if (!text) {
            Debug.LogWarning("Kein Text-Component auf " + objectName);
        }
        return text;
    }
}
