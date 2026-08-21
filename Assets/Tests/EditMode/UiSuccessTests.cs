using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UiSuccessTests
{
    private GameObject successGameObject;
    private UiSuccess success;
    private GameObject timerGameObject;
    private TimerService timerService;
    private GameObject gameEngineGameObject;
    private GameEngineService gameEngineService;
    private GameObject currentTimeTextGameObject;
    private Text currentTimeText;
    private GameObject highscoreTextGameObject;
    private Text highscoreText;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");

        successGameObject = new GameObject("CurrentTimeText");
        success = successGameObject.AddComponent<UiSuccess>();
        currentTimeText = successGameObject.AddComponent<Text>();
        timerGameObject = new GameObject("TimerService");
        timerService = timerGameObject.AddComponent<TimerService>();
        gameEngineGameObject = new GameObject("GameEngineService");
        gameEngineService = gameEngineGameObject.AddComponent<GameEngineService>();
        highscoreTextGameObject = new GameObject("HighscoreText");
        highscoreText = highscoreTextGameObject.AddComponent<Text>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(highscoreTextGameObject);
        Object.DestroyImmediate(gameEngineGameObject);
        Object.DestroyImmediate(timerGameObject);
        Object.DestroyImmediate(successGameObject);

        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [Test]
    public void InitializeDisplaysFinalTimeAndPersonalHighscore()
    {
        timerService.setFinalTime(65.5f);
        PlayerPrefs.SetFloat("PersonalHighscore", 42f);

        success.initialize(timerService, gameEngineService, currentTimeText, highscoreText);

        Assert.AreEqual("01:05.500", currentTimeText.text);
        Assert.AreEqual("00:42.000", highscoreText.text);
    }
}
