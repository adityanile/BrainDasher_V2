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

    public ProgressBarManager progressBarManager;

    public Data data = new Data();

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
        data.userId = MainManager.mainManager.userID;
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

        progressBarManager.ChangeState(index, false);

        playerController.AllowMoveThePlayer();
        //Also Activate Player aura
        gameObject.SetActive(false);
    }

    public void OnClickSubmit()
    {
        if (solution.text != "" && solution.text != " ")
        {
            gameManager.ShowMsg("Question Answered");
            gameManager.safePos = playerController.gameObject.transform.position;

            // Creating QuestionData JSON
            QuestionData queData = new QuestionData();

            queData.givenAns = solution.text;
            queData.correctAns = gameManager.CorrectAnswers[index];
            queData.time = gameManager.clock.GetCurrectTimeJSON();

            data.QueData[index - 1] = queData;

            collisionManager.lastHit.GetComponent<BoxCollider>().enabled = false;

            progressBarManager.ChangeState(index, true);

            ResetData();

            collisionManager.anim.enabled = true;
            gameObject.SetActive(false);
            collisionManager.NextProcess();
        }
        else
        {
            gameManager.ShowMsg("No Valid Arguments");
        }
    }

    void ResetData()
    {
        question.text = "";
        options.text = "";
        solution.text = "";
    }

    [System.Serializable]
    public class Data
    {
        public string userId;
        public string gameCompletionTime;
        public QuestionData[] QueData = new QuestionData[5];
    }


    [System.Serializable]
    public class QuestionData
    {
        public string givenAns;
        public string correctAns;
        public string time;
    }
}

