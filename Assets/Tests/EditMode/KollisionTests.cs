using NUnit.Framework;
using UnityEngine;

public class KollisionTests
{
    private GameObject kollisionGameObject;
    private Kollision kollision;
    private GameObject otherGameObject;

    [SetUp]
    public void SetUp()
    {
        kollisionGameObject = new GameObject("Kollision");
        kollision = kollisionGameObject.AddComponent<Kollision>();
    }

    [TearDown]
    public void TearDown()
    {
        if (otherGameObject != null)
        {
            Object.DestroyImmediate(otherGameObject);
        }
        Object.DestroyImmediate(kollisionGameObject);
    }

    [Test]
    public void IsDeadlyReturnsTrueForObjectOnDeadlyLayer()
    {
        otherGameObject = new GameObject("DeadlyObject");
        otherGameObject.layer = 7;
        kollision.deadlyLayers = 1 << 7;

        Assert.IsTrue(kollision.isDeadly(otherGameObject));
    }

    [Test]
    public void IsDeadlyReturnsFalseForObjectOutsideDeadlyLayers()
    {
        otherGameObject = new GameObject("SafeObject");
        otherGameObject.layer = 7;
        kollision.deadlyLayers = 1 << 6;

        Assert.IsFalse(kollision.isDeadly(otherGameObject));
    }

    [Test]
    public void IsDeadlyReturnsFalseWhenDeadlyLayerMaskIsEmpty()
    {
        otherGameObject = new GameObject("SafeObject");
        otherGameObject.layer = 7;
        kollision.deadlyLayers = 0;

        Assert.IsFalse(kollision.isDeadly(otherGameObject));
    }
}
