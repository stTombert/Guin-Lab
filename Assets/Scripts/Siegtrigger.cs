using UnityEngine;

public class Siegtrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        string tag = other.gameObject.tag;
        if (name == "Spielfigur") {
            GameEngineService gameEngineService = FindObjectOfType<GameEngineService>();
            if (!gameEngineService) {
                Debug.LogError($"{nameof(GameEngineService)} not found in the scene, the win cannot be registered.", this);
                return;
            }
            gameEngineService.wonGame();
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {

    }
}