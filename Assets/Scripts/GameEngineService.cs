using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameEngineService : MonoBehaviour
{
    private const string SuccessSceneName = "SuccessScrene";
    private const string PauseSceneName = "PauseMenu";

    // Renders the pause menu above the canvases of the level it is loaded on top of.
    private const int PauseCanvasSortingOrder = 100;

    public string sceneName;
    public float personalHighscore;
    private TimerService timerService;

    void Start() {
        timerService = Services.Require<TimerService>(this, "the timer and the highscore will not work");
        personalHighscore = getPersonalHighscore();
    }

    // The level is restarted, so the run counts as lost and the timer starts from zero again.
    public void endGame() {
        Debug.Log("GameOver");
        if (timerService) {
            timerService.stopTime();
        }
        Time.timeScale = 1f;
        loadScene(SceneManager.GetActiveScene().name);
    }

    public void wonGame() {
        Debug.Log("Win");

        if (!timerService) {
            Debug.LogError($"Cannot finish the run without a {nameof(TimerService)}.", this);
            return;
        }

        Time.timeScale = 1f;
        // 1. Stop timer
        timerService.stopTime();
        // 2. Set final time
        float finalTime = timerService.currentTime;
        timerService.setFinalTime(finalTime);
        // 3. Set new Highscore, if possible
        setPersonalHighscore(finalTime);
        // 4. FInally Load Success Screne
        loadScene(SuccessSceneName);
    }

    // Keeps the level alive underneath and freezes it, so resuming continues the run
    // instead of restarting it.
    public void pauseGame() {
        if (SceneManager.GetSceneByName(PauseSceneName).isLoaded) {
            return;
        }
        if (!Application.CanStreamedLevelBeLoaded(PauseSceneName)) {
            Debug.LogError($"Scene '{PauseSceneName}' is missing from the build settings, cannot pause.", this);
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
        loadScene(sceneName);
    }

    public void resetHighscore() {
        GameStorage.ClearPersonalHighscore();
        personalHighscore = 0;
    }

    public float getPersonalHighscore() {
        return GameStorage.PersonalHighscore;
    }

    public void setPersonalHighscore(float newHighscore) {
        if (personalHighscore > newHighscore || personalHighscore == 0) {
            GameStorage.PersonalHighscore = newHighscore;
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
        // A scene loaded while the game is paused would stay frozen.
        Time.timeScale = 1f;
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
