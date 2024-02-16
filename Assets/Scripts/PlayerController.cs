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

    public bool motionActive = true;

    public ParticleSystem skipAura;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (motionActive)
        {
            MoveVertically();
            MoveHorizontally();
        }
    }

    public void StartSkipAura()
    {
        skipAura.gameObject.SetActive(true);
    }
    public void StopSkipAura()
    {
        skipAura.gameObject.SetActive(false);
    }

    void MoveVertically()
    {
        // For Forward and Backward Movement
        if (verticalInput > 0)
        {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (verticalInput < 0)
        {
            rb.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    void MoveHorizontally()
    {
        if (horizontalInput > 0)
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (horizontalInput < 0)
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    public void StopThePlayer()
    {
        motionActive = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void AllowMoveThePlayer()
    {
        motionActive = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

}
