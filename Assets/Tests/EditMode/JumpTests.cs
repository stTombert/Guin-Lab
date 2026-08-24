using NUnit.Framework;
using UnityEngine;

public class JumpTests
{
    private GameObject jumpGameObject;
    private Jump jump;

    [SetUp]
    public void SetUp()
    {
        jumpGameObject = new GameObject("Jump");
        jump = jumpGameObject.AddComponent<Jump>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(jumpGameObject);
    }

    [Test]
    public void CalculateJumpReturnsScaledAmountWhenButtonIsPressed()
    {
        Assert.AreEqual(10f, jump.calculateJump(true, 0.5f));
    }

    [Test]
    public void CalculateJumpReturnsZeroWhenButtonIsNotPressed()
    {
        Assert.AreEqual(0f, jump.calculateJump(false, 0.5f));
    }
}
