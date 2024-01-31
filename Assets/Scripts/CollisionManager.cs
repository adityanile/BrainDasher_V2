using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;

    public float repulsiveForce = 10;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Colliding with a outer wall will reset the position to last safespot
        if (collision.gameObject.CompareTag("Barrier"))
        {
            Debug.Log("Hit a Barrier");
            gameObject.transform.position = gameManager.safePos;
        }

    }
}
