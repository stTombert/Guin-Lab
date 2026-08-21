using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kollision : MonoBehaviour
{
    public Movement playerMovement;
    private void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log("Hier ist was passiert");
        Services.GameEngine.endGame();
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Gewonnen hit");
        Debug.Log(hit);
        
    }
}
