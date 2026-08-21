using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class TimerServiceTests
{
    private GameObject timerGameObject;
    private TimerService timerService;
    private GameObject textGameObject;
    private Text text;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");

        timerGameObject = new GameObject("TimerService");
        timerService = timerGameObject.AddComponent<TimerService>();

        textGameObject = new GameObject("TimerText");
        text = textGameObject.AddComponent<Text>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(textGameObject);
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

    [TestCase(0f, "00:00.000")]
    [TestCase(65.5f, "01:05.500")]
    [TestCase(3665.5f, "01:05.500")]
    public void DisplayTimeUsesDefaultFormat(float timeToDisplay, string expectedText)
    {
        timerService.DisplayTime(text, timeToDisplay);

        Assert.AreEqual(expectedText, text.text);
    }

    [Test]
    public void DisplayTimeUsesExplicitCustomFormat()
    {
        timerService.DisplayTime(text, 65.5f, @"hh\:mm\:ss\.ff");

        Assert.AreEqual("00:01:05.50", text.text);
    }
}
