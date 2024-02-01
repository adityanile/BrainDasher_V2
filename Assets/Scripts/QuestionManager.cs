using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI options;
    private int index;

    public TMP_InputField solution;

    private GameManager gameManager;
    private CollisionManager collisionManager;
    private PlayerController playerController;

    private void Awake()
    {
        if (!gameManager)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if (!collisionManager)
        {
            collisionManager = GameObject.Find("Player").GetComponent<CollisionManager>();
        }
        if (!playerController)
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        ResetData();
    }

    public void SetQuestion(int _index, string _question, string _options)
    {
        index = _index;
        question.text = _question;
        options.text = _options;
    }

    public void OnClickSkip()
    {
        gameManager.ShowMsg("Question Skipped");

        ResetData();

        playerController.AllowMoveThePlayer();
        //Also Activate Player aura
        gameObject.SetActive(false);
    }

    public void OnClickSubmit()
    {
        gameManager.ShowMsg("Question Answered");

        gameManager.safePos = playerController.gameObject.transform.position;
        gameManager.QuestionDone.Add(index, solution.text);

        collisionManager.lastHit.GetComponent<BoxCollider>().enabled = false;

        ResetData();

        collisionManager.anim.enabled = true;
        gameObject.SetActive(false);
        collisionManager.NextProcess();
    }

    void ResetData()
    {
        question.text = "";
        options.text = "";
        solution.text = "";
    }

}
