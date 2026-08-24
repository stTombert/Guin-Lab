using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jump = 0;

    // Update is called once per frame
    void Update()
    {
        jump = calculateJump(Input.GetButton("Jump"), Time.deltaTime);
        transform.Translate(0, jump, 0);
    }

    public float calculateJump(bool isJumpPressed, float deltaTime)
    {
        if (isJumpPressed) {
            return 20 * deltaTime;
        } else {
            return 0;
        }
    }
}
