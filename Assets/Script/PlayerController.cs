using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int xInput = 0;
    private int lastXInput = 0;
    private int zInput = 0;
    private int lastZInput = 0;
    public float speed = 10.0f;
    public float jumpStrength = 10.0f;

    private bool isOnGround = true;
    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
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
        Vector3 moveVector = (Vector3.forward * zInput * Time.deltaTime + Vector3.right * xInput * Time.deltaTime) * speed;
        moveVector.y = body.velocity.y;
        body.velocity = moveVector;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            body.velocity = new Vector3(body.velocity.x, jumpStrength, body.velocity.z);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
