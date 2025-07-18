using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float sprintSpeed = 10f;
    private float currentSpeed;
    public float mouseSensitivity = 2.0f;

    private float rotationX = 0;

    void Start()
    {
        currentSpeed = Speed;
    }
    void Update()
    {
            // Check if the player is pressing the Left Shift key for sprinting
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
        currentSpeed = Speed;
        }

         // Call a method to handle movement using currentSpeed
        MovePlayer();
    }

    void MovePlayer()
    {
        // Move the player with WASD
        float moveHorizontal = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float moveVertical = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

        transform.Translate(moveHorizontal, 0, moveVertical);

        // Rotate the player with the mouse (Horizontal)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        // Rotate the camera with the mouse (Vertical)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
