using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UiCanvasScreneTests
{
    private GameObject canvasGameObject;
    private UiCanvasScrene canvas;
    private GameObject timerGameObject;
    private TimerService timerService;
    private GameObject gameEngineGameObject;
    private GameEngineService gameEngineService;
    private GameObject timerTextGameObject;
    private Text timerText;
    private GameObject highscoreTextGameObject;
    private Text highscoreText;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");

        canvasGameObject = new GameObject("Canvas");
        canvas = canvasGameObject.AddComponent<UiCanvasScrene>();
        timerGameObject = new GameObject("TimerService");
        timerService = timerGameObject.AddComponent<TimerService>();
        gameEngineGameObject = new GameObject("GameEngineService");
        gameEngineService = gameEngineGameObject.AddComponent<GameEngineService>();
        timerTextGameObject = new GameObject("TimerText");
        timerText = timerTextGameObject.AddComponent<Text>();
        highscoreTextGameObject = new GameObject("PersonalBestText");
        highscoreText = highscoreTextGameObject.AddComponent<Text>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(highscoreTextGameObject);
        Object.DestroyImmediate(timerTextGameObject);
        Object.DestroyImmediate(gameEngineGameObject);
        Object.DestroyImmediate(timerGameObject);
        Object.DestroyImmediate(canvasGameObject);

        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [Test]
    public void InitializeDisplaysPersonalHighscore()
    {
        PlayerPrefs.SetFloat("PersonalHighscore", 65.5f);

        canvas.initialize(timerService, gameEngineService, timerText, highscoreText);

        Assert.AreEqual("01:05.500", highscoreText.text);
    }

    [Test]
    public void RefreshTimerTextDoesNothingWhenTimerIsDisabled()
    {
        canvas.initialize(timerService, gameEngineService, timerText, highscoreText);
        timerText.text = "unchanged";

        canvas.refreshTimerText();

        Assert.AreEqual("unchanged", timerText.text);
    }

    [Test]
    public void RefreshTimerTextDisplaysCurrentTimeWhenTimerIsEnabled()
    {
        canvas.initialize(timerService, gameEngineService, timerText, highscoreText);
        timerService.currentTime = 65.5f;
        timerService.startTime();

        canvas.refreshTimerText();

        Assert.AreEqual("01:05.500", timerText.text);
    }
}
