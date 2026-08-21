using UnityEngine;

public class Siegtrigger : MonoBehaviour
{
    public GameEngineService gameEngineService;

    private void OnTriggerEnter(Collider other)
    {
        string name = other.gameObject.name;
        if (isPlayer(name)) {
            GameEngineService service = gameEngineService;
            if (service == null) {
                service = FindObjectOfType<GameEngineService>();
            }
            service.wonGame();
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