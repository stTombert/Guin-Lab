using UnityEngine;
using UnityEngine.UI;

public class UiSuccess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimerService timerService = Services.Require<TimerService>(this, "the result times stay empty");
        GameEngineService gameEngineService = Services.Require<GameEngineService>(this, "the result times stay empty");
        if (!timerService || !gameEngineService) {
            return;
        }

        Text currentTimeText = GetComponent<Text>();
        if (!currentTimeText) {
            Debug.LogError($"No {nameof(Text)} component on this GameObject, cannot show the final time.", this);
        } else {
            UiText.DisplayTime(currentTimeText, timerService.getFinalTime());
        }

        Text highscoreText = UiText.Find("HighscoreText", this);
        if (highscoreText) {
            UiText.DisplayTime(highscoreText, gameEngineService.getPersonalHighscore());
        }
    }
}
