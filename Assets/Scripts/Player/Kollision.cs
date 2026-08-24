using UnityEngine;

public class Kollision : MonoBehaviour
{
    public Movement playerMovement;

    // Only collisions with these layers are deadly. Ground and walls have to stay out of the
    // mask, otherwise simply standing on the floor ends the game.
    public LayerMask deadlyLayers;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (!isDeadly(collisionInfo.gameObject)) {
            return;
        }

        GameEngineService gameEngineService = Services.Require<GameEngineService>(this, "the game cannot be ended");
        if (gameEngineService) {
            gameEngineService.endGame();
        }
    }

    public bool isDeadly(GameObject other) {
        return (deadlyLayers.value & (1 << other.layer)) != 0;
    }
}
