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
    private PlayerController playerController;

    public GameObject questionWindow;

    public GameObject lastHit;

    [SerializeField]
    private float animationTime = 1.5f;
    public Animator anim;

    public ProgressBarManager progressBar;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody>();

        questionWindow.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Colliding with a outer wall will reset the position to last safespot
        if (collision.gameObject.CompareTag("Barrier"))
        {
            gameObject.transform.position = gameManager.safePos;
        }

        // Normal Collision with Final Gate
        if (collision.gameObject.CompareTag("FinalGate"))
        {
            gameManager.TouchedLastGate();
        }
    }

    // Managing Gate Collision in Here
    private void OnTriggerEnter(Collider other)
    {
        
            // Managing Gate1 collision
            if (other.gameObject.CompareTag("Gate1"))
            {
                anim = other.gameObject.GetComponent<Animator>();
                lastHit = other.gameObject;

                playerController.StopThePlayer();

                questionWindow.SetActive(true);
                questionWindow.GetComponent<QuestionManager>().SetQuestion(1, "Question 1", "a b c d");

            }

            // Managing Gate2 collision
            if (other.gameObject.CompareTag("Gate2"))
            {
                lastHit = other.gameObject;
                anim = other.gameObject.GetComponent<Animator>();

                playerController.StopThePlayer();

                questionWindow.SetActive(true);
                questionWindow.GetComponent<QuestionManager>().SetQuestion(2, "Question 2", "a b c d");

            }


            // Managing Gate3 collision
            if (other.gameObject.CompareTag("Gate3"))
            {
                lastHit = other.gameObject;
                anim = other.gameObject.GetComponent<Animator>();

                playerController.StopThePlayer();

                questionWindow.SetActive(true);
                questionWindow.GetComponent<QuestionManager>().SetQuestion(3, "Question 3", "a b c d");

            }


            // Managing Gate4 collision
            if (other.gameObject.CompareTag("Gate4"))
            {
                lastHit = other.gameObject;
                anim = other.gameObject.GetComponent<Animator>();

                playerController.StopThePlayer();

                questionWindow.SetActive(true);
                questionWindow.GetComponent<QuestionManager>().SetQuestion(4, "Question 4", "a b c d");

            }


            // Managing Gate5 collision
            if (other.gameObject.CompareTag("Gate5"))
            {
                lastHit = other.gameObject;
                anim = other.gameObject.GetComponent<Animator>();

                playerController.StopThePlayer();

                questionWindow.SetActive(true);
                questionWindow.GetComponent<QuestionManager>().SetQuestion(5, "Question 5", "a b c d");

            }
        
    }

    public void NextProcess()
    {
        StartCoroutine(StopAnim(anim, anim.gameObject));
    }

    public IEnumerator StopAnim(Animator anim, GameObject gate)
    {
        yield return new WaitForSeconds(animationTime);
        anim.enabled = false;

        playerController.AllowMoveThePlayer();
    }
}
