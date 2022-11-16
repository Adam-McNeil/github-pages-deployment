using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private int xInput = 0;
    private int lastXInput = 0;
    private int zInput = 0;
    private int lastZInput = 0;
    private int yInput = 0;
    private int lastYInput = 0;
    public float speed = 0.01f;

    private float xSensitivity = 20;
    private float ySensitivity = 20;
    private float yRotation = 0;
    private float xRotation = 0;
    private float zRotation = 0;
    private float rotationDelta = 0.1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        RotateCamera();
    }

    void RotateCamera()
    {
        float xMouse = Input.GetAxis("Mouse X") * Time.deltaTime * xSensitivity;
        float yMouse = Input.GetAxis("Mouse Y") * Time.deltaTime * ySensitivity;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            zRotation -= rotationDelta;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            zRotation += rotationDelta;
        }
        yRotation += xMouse;
        xRotation -= yMouse;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    void Move()
    {
        xInput = 0;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            xInput = -lastXInput;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            xInput = -1;
            lastXInput = xInput;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xInput = 1;
            lastXInput = xInput;
        }

        zInput = 0;
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W))
        {
            zInput = -lastZInput;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zInput = -1;
            lastZInput = zInput;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            zInput = 1;
            lastZInput = zInput;
        }

        yInput = 0;
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            yInput = -lastYInput;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            yInput = -1;
            lastYInput = yInput;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            yInput = 1;
            lastYInput = yInput;
        }

        Vector3 moveVector = (transform.forward * zInput + transform.right * xInput + Vector3.up * yInput).normalized * speed;
        transform.Translate(moveVector);
    }
}
