using NUnit.Framework;
using UnityEngine;

public class GameStorageTests
{
    [SetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [TearDown]
    public void TearDown()
    {
        PlayerPrefs.DeleteKey("PersonalHighscore");
        PlayerPrefs.DeleteKey("finalTime");
    }

    [Test]
    public void StoragePropertiesReturnZeroWhenNoValuesAreStored()
    {
        Assert.AreEqual(0f, GameStorage.PersonalHighscore);
        Assert.AreEqual(0f, GameStorage.FinalTime);
    }

    [Test]
    public void PersonalHighscoreRoundTripsThroughStorage()
    {
        GameStorage.PersonalHighscore = 42.75f;

        Assert.AreEqual(42.75f, GameStorage.PersonalHighscore);
    }

    [Test]
    public void FinalTimeRoundTripsThroughStorage()
    {
        GameStorage.FinalTime = 65.5f;

        Assert.AreEqual(65.5f, GameStorage.FinalTime);
    }

    [Test]
    public void ClearPersonalHighscoreDoesNotChangeFinalTime()
    {
        GameStorage.PersonalHighscore = 42f;
        GameStorage.FinalTime = 65.5f;

        GameStorage.ClearPersonalHighscore();

        Assert.AreEqual(0f, GameStorage.PersonalHighscore);
        Assert.AreEqual(65.5f, GameStorage.FinalTime);
    }
}
