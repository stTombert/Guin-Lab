using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameEngineService : MonoBehaviour
{
    private const string PauseSceneName = "PauseMenu";
    private const string SuccessSceneName = "SuccessScrene";
    private const string HighscoreKey = "PersonalHighscore";

    // Renders the pause menu above the canvases of the level it is loaded on top of.
    private const int PauseCanvasSortingOrder = 100;

    public string sceneName;
    public float personalHighscore;
    private TimerService timerService;

    void Start() {
        timerService = FindObjectOfType<TimerService>();
        personalHighscore = getPersonalHighscore();
    }

    // The level is restarted, so the run counts as lost and the timer starts from zero again.
    public void endGame() {
        Debug.Log("GameOver");
        if (timerService) {
            timerService.stopTime();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void wonGame() {
        Time.timeScale = 1f;
        timerService.stopTime();
        float finalTime = timerService.currentTime;
        timerService.setFinalTime(finalTime);
        setPersonalHighscore(finalTime);
        SceneManager.LoadScene(SuccessSceneName);
    }

    // Keeps the level alive underneath and freezes it, so resuming continues the run
    // instead of restarting it.
    public void pauseGame() {
        if (SceneManager.GetSceneByName(PauseSceneName).isLoaded) {
            return;
        }
        Time.timeScale = 0f;
        SceneManager.sceneLoaded += onPauseSceneLoaded;
        SceneManager.LoadScene(PauseSceneName, LoadSceneMode.Additive);
    }

    public void resumeGame() {
        Scene pauseScene = SceneManager.GetSceneByName(PauseSceneName);
        if (pauseScene.isLoaded) {
            SceneManager.UnloadSceneAsync(pauseScene);
        }
        Time.timeScale = 1f;
    }

    public void goToScene()
    {
        if (string.IsNullOrEmpty(sceneName)) {
            Debug.LogError("sceneName is not set on " + gameObject.name);
            return;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
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
        if (PlayerPrefs.HasKey(HighscoreKey) && PlayerPrefs.GetFloat(HighscoreKey) <= newHighscore) {
            return;
        }
        PlayerPrefs.SetFloat(HighscoreKey, newHighscore);
        PlayerPrefs.Save();
        personalHighscore = newHighscore;
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

    // The pause scene is authored as a standalone scene and therefore ships with its own
    // camera, audio listener and event system, which would fight with the ones of the level
    // it is loaded on top of.
    private void onPauseSceneLoaded(Scene scene, LoadSceneMode mode) {
        SceneManager.sceneLoaded -= onPauseSceneLoaded;
        if (scene.name != PauseSceneName) {
            return;
        }

        bool hasEventSystemOutsidePauseScene = false;
        foreach (EventSystem eventSystem in FindObjectsOfType<EventSystem>()) {
            if (eventSystem.gameObject.scene != scene) {
                hasEventSystemOutsidePauseScene = true;
                break;
            }
        }

        foreach (GameObject root in scene.GetRootGameObjects()) {
            foreach (Camera camera in root.GetComponentsInChildren<Camera>(true)) {
                camera.enabled = false;
            }
            foreach (AudioListener audioListener in root.GetComponentsInChildren<AudioListener>(true)) {
                audioListener.enabled = false;
            }
            foreach (Canvas canvas in root.GetComponentsInChildren<Canvas>(true)) {
                canvas.sortingOrder = PauseCanvasSortingOrder;
            }
            if (hasEventSystemOutsidePauseScene) {
                foreach (EventSystem eventSystem in root.GetComponentsInChildren<EventSystem>(true)) {
                    eventSystem.gameObject.SetActive(false);
                }
            }
        }
    }
}
