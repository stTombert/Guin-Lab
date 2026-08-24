using System.Text.RegularExpressions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ServicesTests
{
    private GameObject contextGameObject;
    private GameObject firstServiceGameObject;
    private GameObject secondServiceGameObject;

    [SetUp]
    public void SetUp()
    {
        contextGameObject = new GameObject("ServicesTestContext");
    }

    [TearDown]
    public void TearDown()
    {
        if (secondServiceGameObject != null)
        {
            Object.DestroyImmediate(secondServiceGameObject);
        }
        if (firstServiceGameObject != null)
        {
            Object.DestroyImmediate(firstServiceGameObject);
        }
        Object.DestroyImmediate(contextGameObject);
    }

    [Test]
    public void GetReturnsNullWhenServiceIsAbsent()
    {
        Assert.IsNull(Services.Get<GameEngineService>());
    }

    [Test]
    public void GetReturnsTheSceneServiceInstanceWhenPresent()
    {
        firstServiceGameObject = new GameObject("GameEngineService");
        GameEngineService expectedService = firstServiceGameObject.AddComponent<GameEngineService>();

        GameEngineService result = Services.Get<GameEngineService>();

        Assert.AreSame(expectedService, result);
    }

    [Test]
    public void GetReturnsFreshServiceAfterCachedInstanceIsDestroyed()
    {
        firstServiceGameObject = new GameObject("FirstGameEngineService");
        GameEngineService firstService = firstServiceGameObject.AddComponent<GameEngineService>();
        Assert.AreSame(firstService, Services.Get<GameEngineService>());

        Object.DestroyImmediate(firstServiceGameObject);
        firstServiceGameObject = null;
        secondServiceGameObject = new GameObject("SecondGameEngineService");
        GameEngineService secondService = secondServiceGameObject.AddComponent<GameEngineService>();

        Assert.AreSame(secondService, Services.Get<GameEngineService>());
    }

    [Test]
    public void RequireLogsErrorNamingMissingService()
    {
        LogAssert.Expect(LogType.Error, new Regex("GameEngineService not found.*"));

        Assert.IsNull(Services.Require<GameEngineService>(contextGameObject, "the test service is unavailable"));
    }
}
