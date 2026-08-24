using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class UiTextTests
{
    private GameObject textGameObject;
    private Text text;
    private GameObject foundGameObject;

    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");

        textGameObject = new GameObject("Text");
        text = textGameObject.AddComponent<Text>();
    }

    [TearDown]
    public void TearDown()
    {
        if (foundGameObject != null)
        {
            Object.DestroyImmediate(foundGameObject);
        }
        Object.DestroyImmediate(textGameObject);

        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [TestCase(0f, "00:00.000")]
    [TestCase(65.5f, "01:05.500")]
    [TestCase(3665.5f, "01:05.500")]
    public void DisplayTimeUsesDefaultFormat(float timeToDisplay, string expectedText)
    {
        UiText.DisplayTime(text, timeToDisplay);

        Assert.AreEqual(expectedText, text.text);
    }

    [Test]
    public void DisplayTimeUsesExplicitCustomFormat()
    {
        UiText.DisplayTime(text, 65.5f, @"hh\:mm\:ss\.ff");

        Assert.AreEqual("00:01:05.50", text.text);
    }

    [Test]
    public void DisplayTimeLogsErrorForNullTarget()
    {
        LogAssert.Expect(LogType.Error, new Regex("No text target given.*"));

        UiText.DisplayTime(null, 1f);
    }

    [Test]
    public void FindReturnsTextForExistingNamedGameObject()
    {
        foundGameObject = new GameObject("NamedText");
        Text expectedText = foundGameObject.AddComponent<Text>();

        Text result = UiText.Find("NamedText");

        Assert.AreSame(expectedText, result);
    }

    [Test]
    public void FindReturnsNullAndLogsErrorForMissingGameObject()
    {
        LogAssert.Expect(LogType.Error, new Regex("GameObject 'MissingText' not found.*"));

        Text result = UiText.Find("MissingText");

        Assert.IsNull(result);
    }

    [Test]
    public void FindLogsErrorWhenGameObjectHasNoTextComponent()
    {
        foundGameObject = new GameObject("NoText");
        LogAssert.Expect(LogType.Error, new Regex("GameObject 'NoText' has no Text component\\."));

        Text result = UiText.Find("NoText");

        Assert.IsNull(result);
    }
}
