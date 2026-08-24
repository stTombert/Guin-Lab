using UnityEngine;

public class Siegtrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        string tag = other.gameObject.tag;
        if (isPlayer(name)) {
            GameEngineService gameEngineService = Services.Require<GameEngineService>(this, "the win cannot be registered");
            if (gameEngineService) {
                gameEngineService.wonGame();
            }
        }
    }

    public bool isPlayer(string objectName)
    {
        return objectName == "Spielfigur";
    }

    private void OnCollisionExit(Collision collisionInfo)
    {

    }
}
