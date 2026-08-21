using UnityEngine;
using UnityEngine.UI;

public class UiSuccess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UiText.DisplayTime(GetComponent<Text>(), Services.Timer.getFinalTime());
        UiText.DisplayTime(UiText.Find("HighscoreText"), Services.GameEngine.getPersonalHighscore());
    }
}
