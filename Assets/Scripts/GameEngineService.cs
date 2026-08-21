using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngineService : MonoBehaviour
{
    private const string HighscoreKey = "PersonalHighscore";
    private const string SuccessSceneName = "SuccessScrene";

    public string sceneName;
    static GameObject RunningGameState;
    public string loadState;
    public float personalHighscore;
    private TimerService timerService;

    void Start() {
        Debug.Log("Welcome to the game");
        timerService = FindObjectOfType<TimerService>();
        if (!timerService) {
            Debug.LogError($"{nameof(TimerService)} not found in the scene, the timer and the highscore will not work.", this);
        }
        personalHighscore = getPersonalHighscore();
        Debug.Log(personalHighscore);
    }
    public void endGame() {
        Debug.Log("GameOver");
    }

    public void wonGame() {
        Debug.Log("Win");

        if (!timerService) {
            Debug.LogError($"Cannot finish the run without a {nameof(TimerService)}.", this);
            return;
        }

        // 1. Stop timer
        timerService.stopTime();
        // 2. Set final time
        timerService.setFinalTime(timerService.currentTime);
        // 3. Set new Highscore, if possible
        setPersonalHighscore(timerService.getFinalTime());
        // 4. FInally Load Success Screne
        loadScene(SuccessSceneName);
    }

    public void goToScene()
    {
        if (loadState == "currentGame") {
            Debug.Log("Load state");
        }
        loadScene(sceneName);
    }

    public void resetHighscore() {
        PlayerPrefs.DeleteKey(HighscoreKey);
        PlayerPrefs.Save();
        personalHighscore = 0;
    }

    public float getPersonalHighscore() {
        return PlayerPrefs.GetFloat(HighscoreKey, 0);
    }

    public void setPersonalHighscore(float newHighscore) {
        if (personalHighscore > newHighscore || personalHighscore == 0) {
            PlayerPrefs.SetFloat(HighscoreKey, newHighscore);
            PlayerPrefs.Save();
            personalHighscore = newHighscore;
        }
    }

    private void loadScene(string targetSceneName) {
        if (string.IsNullOrEmpty(targetSceneName)) {
            Debug.LogError("No scene name configured, cannot load a scene.", this);
            return;
        }
        if (!Application.CanStreamedLevelBeLoaded(targetSceneName)) {
            Debug.LogError($"Scene '{targetSceneName}' is missing from the build settings, cannot load it.", this);
            return;
        }
        SceneManager.LoadScene(targetSceneName);
    }

    public void quit() {
        // two ways of ending the game
        // 1. End game in editor
        // 2. End game while playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}