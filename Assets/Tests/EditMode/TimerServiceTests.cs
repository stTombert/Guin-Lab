using NUnit.Framework;
using UnityEngine;

public class TimerServiceTests
{
    private GameObject timerGameObject;
    private TimerService timerService;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");

        timerGameObject = new GameObject("TimerService");
        timerService = timerGameObject.AddComponent<TimerService>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(timerGameObject);

        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [Test]
    public void NewTimerHasDefaultState()
    {
        Assert.AreEqual(0f, timerService.currentTime);
        Assert.IsFalse(timerService.isTimeEnabled);
    }

    [Test]
    public void StartTimeEnablesTimer()
    {
        timerService.startTime();

        Assert.IsTrue(timerService.isTimeEnabled);
    }

    [Test]
    public void StopTimeDisablesTimer()
    {
        timerService.startTime();

        timerService.stopTime();

        Assert.IsFalse(timerService.isTimeEnabled);
    }

    [Test]
    public void StartTimeAfterStopEnablesTimerAgain()
    {
        timerService.startTime();
        timerService.stopTime();

        timerService.startTime();

        Assert.IsTrue(timerService.isTimeEnabled);
    }

    [Test]
    public void SetFinalTimeAndGetFinalTimeRoundTrip()
    {
        timerService.setFinalTime(42.75f);

        Assert.AreEqual(42.75f, timerService.getFinalTime());
    }

    [Test]
    public void GetFinalTimeReturnsZeroWhenNoValueIsStored()
    {
        Assert.AreEqual(0f, timerService.getFinalTime());
    }
}
