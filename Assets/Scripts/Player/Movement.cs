using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 15;
    public float gravity;

    // Falling below this height means the player left the level and the game is over.
    public float fallDeathHeight = -10;

    private CharacterController characterController;
    private float currentGravity = 0;
    private bool hasEnded = false;


    void Start() {
        characterController = gameObject.GetComponent<CharacterController>();
        if (!characterController) {
            Debug.LogError($"No {nameof(CharacterController)} on this GameObject, movement stays disabled.", this);
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallDeathHeight) {
            endGame();
            return;
        }

        Vector3 finalMovement = MoveChar(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) + ApplyGravity(characterController.isGrounded, Time.deltaTime);
        characterController.Move(finalMovement * Time.deltaTime);
    }

    public Vector3 MoveChar(float vertical, float horizontal) {
        Vector3 moveVector = Vector3.zero;

        moveVector += transform.forward * vertical;
        moveVector += transform.right * horizontal;

        moveVector *= moveSpeed;

        return moveVector;
    }

    public Vector3 ApplyGravity(bool isGrounded, float deltaTime) {
        Vector3 gravityMovement = new Vector3(0, -currentGravity, 0);
        currentGravity += gravity * deltaTime;

        if (isGrounded && currentGravity > 1f) {
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

        GameEngineService gameEngineService = Services.Require<GameEngineService>(this, "the game cannot be ended");
        if (gameEngineService) {
            gameEngineService.endGame();
        }
    }
}
