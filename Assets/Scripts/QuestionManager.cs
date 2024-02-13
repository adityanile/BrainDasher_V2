using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI question;
    public TextMeshProUGUI options;
    public TextMeshProUGUI tries;
    public TextMeshProUGUI points;

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

    public void SetQuestion(int _index, string _question, string _options, string _tries, string _points)
    {
        index = _index;
        question.text = _question;
        options.text = _options;

        tries.text = _tries;
        points.text = _points;
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
            ShowFinalTryWarning();

            if (gameManager.questionTries[index] > 0)
            {
                if (ValidationAnswer(solution.text, gameManager.CorrectAnswers[index]))
                {
                    SumbitTheSolution();
                }
                else
                {
                    Debug.Log("Wrong Answer");
                    gameManager.ForAWrongAnswer(index);
                    UpdateTriesScore();

                    if (gameManager.questionTries[index] == 0)
                    {
                        AfterNoTries();
                    }
                }
            }
            else
            {
                AfterNoTries();
            }
        }
        else
        {
            gameManager.ShowMsg("No Valid Arguments");

        }
    }
 
    void AfterNoTries()
    {
        gameManager.ShowMsg("Oops! No Enough Tries Available");
        
        NecessaryProcess();
        playerController.AllowMoveThePlayer();

        collisionManager.DestroyGate();
        return;
    }

    void UpdateTriesScore()
    {
        tries.text = gameManager.questionTries[index].ToString();
        points.text = gameManager.questionScore[index].ToString();
    }

    void SumbitTheSolution()
    {
        gameManager.ShowMsg("Question Answered");
        gameManager.safePos = playerController.gameObject.transform.position;

        // Creating QuestionData JSON
        QuestionData queData = new QuestionData();

        queData.givenAns = solution.text;
        queData.correctAns = gameManager.CorrectAnswers[index];
        queData.time = gameManager.clock.GetCurrectTimeJSON();

        queData.points = gameManager.questionScore[index];

        // Update Score in UI
        gameManager.ManageScore(queData.points);

        data.QueData[index - 1] = queData;

        collisionManager.lastHit.GetComponent<BoxCollider>().enabled = false;

        progressBarManager.ChangeState(index, true);

        ResetData();

        collisionManager.anim.enabled = true;
        gameObject.SetActive(false);
        collisionManager.NextProcess();
    }

    void NecessaryProcess()
    {
        collisionManager.lastHit.GetComponent<BoxCollider>().enabled = false;

        progressBarManager.ChangeState(index, false);

        ResetData();
        gameObject.SetActive(false);
    }

    void ShowFinalTryWarning() 
    {
        if (gameManager.questionTries[index] == 2)
        {
            gameManager.ShowMsg("Final Try");
        }
    }

    bool ValidationAnswer(string givenAns, string correctAns)
    {
        // Write Here
        givenAns.TrimStart();
        givenAns.TrimEnd();

        correctAns.TrimStart();
        correctAns.TrimEnd();

        if(givenAns == correctAns)
        {
            return true;
        }
        else
        {
            return false;
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
        public int totalScore;
        public QuestionData[] QueData = new QuestionData[5];
    }


    [System.Serializable]
    public class QuestionData
    {
        public string givenAns;
        public string correctAns;
        public string time;
        public int points;
    }
}

