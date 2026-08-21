using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kollision : MonoBehaviour
{
    public Movement playerMovement;
    public GameEngineService gameEngineService;
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
    public void endGame() {
        GameEngineService service = gameEngineService;
        if (service == null) {
            service = FindObjectOfType<GameEngineService>();
        }
        service.endGame();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Gewonnen hit");
        Debug.Log(hit);
        
    }
}
