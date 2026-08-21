using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kollision : MonoBehaviour
{
    public Movement playerMovement;
    private void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Hier ist was passiert");
        endGame();
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
    }

    // Finally one method to end the game via the game manager
    private void endGame() {
        GameEngineService gameEngineService = FindObjectOfType<GameEngineService>();
        if (!gameEngineService) {
            Debug.LogError($"{nameof(GameEngineService)} not found in the scene, the game cannot be ended.", this);
            return;
        }
        gameEngineService.endGame();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Gewonnen hit");
        Debug.Log(hit);
        
    }
}
