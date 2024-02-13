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

    public GameObject confirmPopup;
    public float hitoffset = -7;

    [SerializeField]
    private QuestionManager queManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody>();

        questionWindow.SetActive(false);
        confirmPopup.SetActive(false);
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
            confirmPopup.SetActive(true);
        }
    }

    // Confirm Popup Before Final Gate Entry
    public void OnClickFinalSubmit()
    {
        confirmPopup.SetActive(false);

        player.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,
                                                            player.transform.position.z + hitoffset);

        // Final Game Process
        gameManager.clock.StopClock();
        queManager.data.gameCompletionTime = gameManager.clock.GetCurrectTimeJSON();

        playerController.StopThePlayer();
        StartSubmission();
    }

    public void StartSubmission()
    {
        StartCoroutine(gameManager.GenerateCrystals(gameManager.questionAnswered));
    }

    public void OnClickFinalNo()
    {
        confirmPopup.SetActive(false);
        player.gameObject.transform.position = new Vector3( player.transform.position.x, player.transform.position.y,
                                                            player.transform.position.z + hitoffset);

        gameManager.ShowMsg("Back To The Game");
    }


    // Managing Gate Collision in Here
    private void OnTriggerEnter(Collider other)
    {   
            // Managing Gate1 collision
            if (other.gameObject.CompareTag("Gate1"))
            {
                anim = other.gameObject.GetComponent<Animator>();
                
            playerController.StopThePlayer();

                questionWindow.SetActive(true);

                string tries = gameManager.questionTries[1].ToString();
                string points = gameManager.questionScore[1].ToString();

                questionWindow.GetComponent<QuestionManager>().SetQuestion(1, "Question 1", "a b c d",tries, points);

            lastHit = other.gameObject;
            }

            // Managing Gate2 collision
            if (other.gameObject.CompareTag("Gate2"))
            {
                anim = other.gameObject.GetComponent<Animator>();
            
            playerController.StopThePlayer();

                questionWindow.SetActive(true);

                string tries = gameManager.questionTries[2].ToString();
                string points = gameManager.questionScore[2].ToString();

                questionWindow.GetComponent<QuestionManager>().SetQuestion(2, "Question 2", "a b c d", tries, points) ;

            lastHit = other.gameObject;
            }


            // Managing Gate3 collision
            if (other.gameObject.CompareTag("Gate3"))
            {
                anim = other.gameObject.GetComponent<Animator>();

            playerController.StopThePlayer();

                questionWindow.SetActive(true);

                string tries = gameManager.questionTries[3].ToString();
                string points = gameManager.questionScore[3].ToString();

                questionWindow.GetComponent<QuestionManager>().SetQuestion(3, "Question 3", "a b c d", tries, points);

            lastHit = other.gameObject;
            }


            // Managing Gate4 collision
            if (other.gameObject.CompareTag("Gate4"))
            {
                anim = other.gameObject.GetComponent<Animator>();

            playerController.StopThePlayer();

                questionWindow.SetActive(true);

                string tries = gameManager.questionTries[4].ToString();
                string points = gameManager.questionScore[4].ToString();

            questionWindow.GetComponent<QuestionManager>().SetQuestion(4, "Question 4", "a b c d", tries, points);

            lastHit = other.gameObject;
        }


            // Managing Gate5 collision
            if (other.gameObject.CompareTag("Gate5"))
            {
                anim = other.gameObject.GetComponent<Animator>();

            playerController.StopThePlayer();

                questionWindow.SetActive(true);

                string tries = gameManager.questionTries[5].ToString();
                string points = gameManager.questionScore[5].ToString();

            questionWindow.GetComponent<QuestionManager>().SetQuestion(5, "Question 5", "a b c d", tries, points);

            lastHit = other.gameObject;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Contains("Gate"))
        {
            playerController.StopSkipAura();
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

    public void DestroyGate()
    {
        Destroy(lastHit);
        Debug.Log("Destroyed the last Gate");
    }
}
