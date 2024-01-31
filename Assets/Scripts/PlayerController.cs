using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody rb;

    [SerializeField]
    private float horizontalInput;
    [SerializeField]
    private float verticalInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        MoveVertically();
        MoveHorizontally();
    }

    void MoveVertically()
    {
        // For Forward and Backward Movement
        if (verticalInput > 0)
        {
            rb.AddForce(Vector3.forward * speed, ForceMode.Acceleration);
        }

        if (verticalInput < 0)
        {
            rb.AddForce(Vector3.back * speed, ForceMode.Acceleration);
        }
    }

    void MoveHorizontally()
    {
        if (horizontalInput > 0)
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Acceleration);
        }

        if (horizontalInput < 0)
        {
            rb.AddForce(Vector3.left * speed, ForceMode.Acceleration);
        }
    }
}
