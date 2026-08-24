using NUnit.Framework;
using UnityEngine;

public class SiegtriggerTests
{
    private GameObject siegtriggerGameObject;
    private Siegtrigger siegtrigger;

    [SetUp]
    public void SetUp()
    {
        siegtriggerGameObject = new GameObject("Siegtrigger");
        siegtrigger = siegtriggerGameObject.AddComponent<Siegtrigger>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(siegtriggerGameObject);
    }

    [Test]
    public void IsPlayerReturnsTrueForSpielfigur()
    {
        Assert.IsTrue(siegtrigger.isPlayer("Spielfigur"));
    }

    [Test]
    public void IsPlayerReturnsFalseForOtherObjectNames()
    {
        Assert.IsFalse(siegtrigger.isPlayer("Wall"));
    }
}
