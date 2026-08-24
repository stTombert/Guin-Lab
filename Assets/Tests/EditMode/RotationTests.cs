using NUnit.Framework;
using UnityEngine;

public class RotationTests
{
    private GameObject rotationGameObject;
    private Rotation rotation;

    [SetUp]
    public void SetUp()
    {
        rotationGameObject = new GameObject("Rotation");
        rotation = rotationGameObject.AddComponent<Rotation>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(rotationGameObject);
    }

    [Test]
    public void CalculateRotationUsesRightArrow()
    {
        Vector2 result = rotation.calculateRotation(true, false, false, false, 0.5f);

        Assert.AreEqual(10f, result.x);
        Assert.AreEqual(0f, result.y);
    }

    [Test]
    public void CalculateRotationUsesLeftArrow()
    {
        Vector2 result = rotation.calculateRotation(false, true, false, false, 0.5f);

        Assert.AreEqual(-10f, result.x);
        Assert.AreEqual(0f, result.y);
    }

    [Test]
    public void CalculateRotationUsesUpArrow()
    {
        Vector2 result = rotation.calculateRotation(false, false, true, false, 0.5f);

        Assert.AreEqual(0f, result.x);
        Assert.AreEqual(10f, result.y);
    }

    [Test]
    public void CalculateRotationUsesDownArrow()
    {
        Vector2 result = rotation.calculateRotation(false, false, false, true, 0.5f);

        Assert.AreEqual(0f, result.x);
        Assert.AreEqual(-10f, result.y);
    }

    [Test]
    public void CalculateRotationGivesRightArrowPrecedence()
    {
        Vector2 result = rotation.calculateRotation(true, true, true, true, 0.5f);

        Assert.AreEqual(10f, result.x);
        Assert.AreEqual(0f, result.y);
    }

    [Test]
    public void CalculateRotationResetsBothAxesWhenNoArrowIsPressed()
    {
        rotation.x_rota = 10f;
        rotation.y_rota = -10f;

        Vector2 result = rotation.calculateRotation(false, false, false, false, 0.5f);

        Assert.AreEqual(0f, result.x);
        Assert.AreEqual(0f, result.y);
    }

    [Test]
    public void CalculateRotationPreservesOtherAxisWhenOneArrowIsPressed()
    {
        rotation.x_rota = 3f;
        rotation.y_rota = 4f;

        Vector2 result = rotation.calculateRotation(false, false, true, false, 0.5f);

        Assert.AreEqual(3f, result.x);
        Assert.AreEqual(10f, result.y);
    }
}
