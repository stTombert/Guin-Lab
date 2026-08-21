using UnityEngine;

public class Kollision : MonoBehaviour
{
    public Movement playerMovement;

    // Only collisions with these layers are deadly. Ground and walls have to stay out of the
    // mask, otherwise simply standing on the floor ends the game.
    public LayerMask deadlyLayers;

    private GameEngineService gameEngineService;

    void Start() {
        gameEngineService = FindObjectOfType<GameEngineService>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (!isDeadly(collisionInfo.gameObject)) {
            return;
        }
        endGame();
    }

    private bool isDeadly(GameObject other) {
        return (deadlyLayers.value & (1 << other.layer)) != 0;
    }

    // Finally one method to end the game via the game manager
    private void endGame() {
        if (!gameEngineService) {
            Debug.LogError("No GameEngineService in the scene, the game cannot be ended");
            return;
        }
        gameEngineService.endGame();
    }
}
