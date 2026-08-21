using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 15;
    public float gravity;

    private CharacterController characterController;
    private float currentGravity = 0;


    void Start() {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 finalMovement = MoveChar(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) + ApplyGravity(characterController.isGrounded, Time.deltaTime);
        if (!characterController) {
            Debug.Log("Der characterController ist nicht gesetzt");
        } else {
            characterController.Move(finalMovement * Time.deltaTime);
        }
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
        // if (characterController.attachedRigidbody.position.y < -1) {
        //     endGame();
        // }

        return gravityMovement;
    }

    // Finally one method to end the game via the game manager
    private void endGame() {
        FindObjectOfType<GameEngineService>().endGame();
    }

    private void OnControllerColliderHit() {
        // Debug.Log("Hier ist was passiert");
    }
}
