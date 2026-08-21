using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 15;
    public float gravity;

    // Falling below this height means the player left the level and the game is over.
    public float fallDeathHeight = -10;

    private CharacterController characterController;
    private GameEngineService gameEngineService;
    private float currentGravity = 0;
    private bool hasEnded = false;


    void Start() {
        characterController = gameObject.GetComponent<CharacterController>();
        gameEngineService = FindObjectOfType<GameEngineService>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterController) {
            Debug.Log("Der characterController ist nicht gesetzt");
            return;
        }

        if (transform.position.y < fallDeathHeight) {
            endGame();
            return;
        }

        Vector3 finalMovement = MoveChar() + ApplyGravity();
        characterController.Move(finalMovement * Time.deltaTime);
    }

    Vector3 MoveChar() {
        Vector3 moveVector = Vector3.zero;

        moveVector += transform.forward * Input.GetAxis("Vertical");
        moveVector += transform.right * Input.GetAxis("Horizontal");

        moveVector *= moveSpeed;

        return moveVector;
    }

    Vector3 ApplyGravity() {
        Vector3 gravityMovement = new Vector3(0, -currentGravity, 0);
        currentGravity += gravity * Time.deltaTime;

        if (characterController.isGrounded && currentGravity > 1f) {
            currentGravity = 1f;
        }

        return gravityMovement;
    }

    // Finally one method to end the game via the game manager
    private void endGame() {
        if (hasEnded) {
            return;
        }
        hasEnded = true;
        if (!gameEngineService) {
            Debug.LogError("No GameEngineService in the scene, the game cannot be ended");
            return;
        }
        gameEngineService.endGame();
    }
}
