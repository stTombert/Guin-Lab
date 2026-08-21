using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float x_rota = 0;
    public float y_rota = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rotation = calculateRotation(
            Input.GetKey(KeyCode.RightArrow),
            Input.GetKey(KeyCode.LeftArrow),
            Input.GetKey(KeyCode.UpArrow),
            Input.GetKey(KeyCode.DownArrow),
            Time.deltaTime);
        x_rota = rotation.x;
        y_rota = rotation.y;
        // transform.rotation = transform.rotation += transform.Rotate(x_rota);
        transform.Rotate(x_rota, y_rota, 0);
    }

    public Vector2 calculateRotation(
        bool isRightPressed,
        bool isLeftPressed,
        bool isUpPressed,
        bool isDownPressed,
        float deltaTime)
    {
        float xRotation = x_rota;
        float yRotation = y_rota;

        if (isRightPressed) {
            xRotation = 20 * deltaTime;
        } else if (isLeftPressed) {
            xRotation = -20 * deltaTime;
        } else if (isUpPressed) {
            yRotation = 20 * deltaTime;
        } else if (isDownPressed) {
            yRotation = -20 * deltaTime;
        } else {
            xRotation = 0;
            yRotation = 0;
        }

        return new Vector2(xRotation, yRotation);
    }
}
