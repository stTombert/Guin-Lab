using UnityEngine;

public class Siegtrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Spielfigur") {
            return;
        }

        GameEngineService gameEngineService = FindObjectOfType<GameEngineService>();
        if (!gameEngineService) {
            Debug.LogWarning("Kein GameEngineService in der Szene gefunden");
            return;
        }
        gameEngineService.wonGame();
    }

    private void OnCollisionExit(Collision collisionInfo)
    {

    }
}