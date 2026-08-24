using NUnit.Framework;
using UnityEngine;

public class MovementTests
{
    private GameObject movementGameObject;
    private Movement movement;

    [SetUp]
    public void SetUp()
    {
        movementGameObject = new GameObject("Movement");
        movement = movementGameObject.AddComponent<Movement>();
        movement.transform.forward = new Vector3(0f, 0f, 1f);
        movement.transform.right = new Vector3(1f, 0f, 0f);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(movementGameObject);
    }

    [Test]
    public void MoveCharUsesVerticalAndHorizontalAxesAndMoveSpeed()
    {
        Vector3 result = movement.MoveChar(2f, 3f);

        Assert.AreEqual(45f, result.x);
        Assert.AreEqual(0f, result.y);
        Assert.AreEqual(30f, result.z);
    }

    [Test]
    public void ApplyGravityReturnsCurrentGravityBeforeIncrementing()
    {
        movement.gravity = 10f;

        Vector3 firstResult = movement.ApplyGravity(false, 1f);
        Vector3 secondResult = movement.ApplyGravity(false, 1f);

        Assert.AreEqual(0f, firstResult.y);
        Assert.AreEqual(-10f, secondResult.y);
    }

    [Test]
    public void ApplyGravityClampsAccumulatedGravityWhenGrounded()
    {
        movement.gravity = 10f;
        movement.ApplyGravity(false, 1f);

        Vector3 groundedResult = movement.ApplyGravity(true, 1f);
        Vector3 clampedResult = movement.ApplyGravity(false, 0f);

        Assert.AreEqual(-10f, groundedResult.y);
        Assert.AreEqual(-1f, clampedResult.y);
    }
}
