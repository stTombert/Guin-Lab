using NUnit.Framework;
using UnityEngine;

public class GameEngineServiceTests
{
    private GameObject gameEngineGameObject;
    private GameEngineService gameEngineService;
    private GameObject timerGameObject;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");

        gameEngineGameObject = new GameObject("GameEngineService");
        gameEngineService = gameEngineGameObject.AddComponent<GameEngineService>();
    }

    [TearDown]
    public void TearDown()
    {
        if (timerGameObject != null)
        {
            Object.DestroyImmediate(timerGameObject);
        }
        Object.DestroyImmediate(gameEngineGameObject);

        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [Test]
    public void GetPersonalHighscoreReturnsZeroWhenNoValueIsStored()
    {
        Assert.AreEqual(0f, gameEngineService.getPersonalHighscore());
    }

    [Test]
    public void SetPersonalHighscoreStoresValueWhenPersonalHighscoreIsZero()
    {
        gameEngineService.personalHighscore = 0f;

        gameEngineService.setPersonalHighscore(42f);

        Assert.AreEqual(42f, gameEngineService.getPersonalHighscore());
    }

    [Test]
    public void SetPersonalHighscoreStoresLowerValueThanCurrentPersonalHighscore()
    {
        gameEngineService.personalHighscore = 100f;

        gameEngineService.setPersonalHighscore(75f);

        Assert.AreEqual(75f, gameEngineService.getPersonalHighscore());
    }

    [Test]
    public void SetPersonalHighscoreDoesNotStoreHigherValueThanCurrentPersonalHighscore()
    {
        gameEngineService.personalHighscore = 100f;

        gameEngineService.setPersonalHighscore(125f);

        Assert.AreEqual(0f, gameEngineService.getPersonalHighscore());
    }

    [Test]
    public void ResetHighscoreDeletesStoredValue()
    {
        PlayerPrefs.SetFloat("PersonalHighscore", 42f);

        gameEngineService.resetHighscore();

        Assert.AreEqual(0f, gameEngineService.getPersonalHighscore());
    }

    [Test]
    public void ProcessWinStopsTimerPersistsFinalTimeAndUpdatesHighscore()
    {
        timerGameObject = new GameObject("TimerService");
        TimerService timerService = timerGameObject.AddComponent<TimerService>();
        timerService.startTime();
        timerService.currentTime = 42f;
        gameEngineService.personalHighscore = 50f;
        gameEngineService.timerService = timerService;

        gameEngineService.processWin();

        Assert.IsFalse(timerService.isTimeEnabled);
        Assert.AreEqual(42f, timerService.getFinalTime());
        Assert.AreEqual(42f, gameEngineService.getPersonalHighscore());
    }
}
