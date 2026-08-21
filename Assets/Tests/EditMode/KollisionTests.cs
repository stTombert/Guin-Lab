using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class KollisionTests
{
    private GameObject kollisionGameObject;
    private Kollision kollision;
    private GameObject gameEngineGameObject;
    private GameEngineService gameEngineService;

    [SetUp]
    public void SetUp()
    {
        kollisionGameObject = new GameObject("Kollision");
        kollision = kollisionGameObject.AddComponent<Kollision>();
        gameEngineGameObject = new GameObject("GameEngineService");
        gameEngineService = gameEngineGameObject.AddComponent<GameEngineService>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameEngineGameObject);
        Object.DestroyImmediate(kollisionGameObject);
    }

    [Test]
    public void EndGameUsesInjectedGameEngineService()
    {
        kollision.gameEngineService = gameEngineService;
        LogAssert.Expect(LogType.Log, "GameOver");

        kollision.endGame();
    }

    [Test]
    public void EndGameFallsBackToFoundGameEngineService()
    {
        LogAssert.Expect(LogType.Log, "GameOver");

        kollision.endGame();
    }
}
