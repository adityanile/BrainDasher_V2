using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector3 safePos;

    public GameObject msg;

    [SerializeField]
    public Dictionary<int, string> CorrectAnswers = new Dictionary<int, string>();
    public Dictionary<int, int> questionTries = new Dictionary<int, int>();
    public Dictionary<int, int> questionScore = new Dictionary<int, int>();

    public int questionAnswered = 0;

    public Clock clock;

    public TextMeshProUGUI timerui;
    public TextMeshProUGUI userID;

    [SerializeField]
    private QuestionManager queManager;

    private int initialPoints = 50;
    private int decrement = 20;

    public TextMeshProUGUI scoreui;
    private int totalPoints = 0;

    // For Final Gate Crystal Submission In the Portal
    public GameObject[] crystals;
    public float animWaitTime = 2;

    private GameObject player;

    // For Random Question Initailisation
    public QuestionData[] questions = new QuestionData[5];
    public QuestionSplitter questionSplitter;

    private void Awake()
    {
        if (!clock)
        {
            clock = GameObject.Find("GameClock").GetComponent<Clock>();
        }

        // Getting Random Question For the User
        SetQuestionsData();

        userID.text = "Player ID:- " + MainManager.mainManager.userID;

        // Initailise The Score and Tries
        InitData();

        player = GameObject.Find("Player");
    }


    void SetQuestionsData()
    {
        for(int i=0; i < questions.Length; i++) 
        {
            questions[i] = questionSplitter.GetMeAQuestion(i);
            CorrectAnswers.Add(i + 1, questions[i].correctAns.ToString());
        }
    }

    void InitData()
    {
        for (int i = 0; i < 5; i++)
        {
            questionTries[i + 1] = 3;
            questionScore[i + 1] = initialPoints;
        }
    }


    void Start()
    {
        // Start the Clock
        clock.StartClock();
        timerui.text = "Time:- " + clock.DisplayTime();

        ShowMsg("Game Started");

        safePos = new Vector3 (0.0f, 0.0f, 0.0f);

        ManageScore(0);
    }

    private void Update()
    {
        timerui.text = "Time:- " + clock.DisplayTime();
    }

    // Crystals Generation
    public IEnumerator GenerateCrystals(int num)
    {
        num--;

        while (num >= 0)
        {
            Instantiate(crystals[num], player.gameObject.transform.position, crystals[num].transform.rotation);
            num--;
            yield return new WaitForSeconds(animWaitTime);
        }

        Debug.Log("Saved Data");
        TouchedLastGate();
    }

    public void TouchedLastGate()
    {
        queManager.data.totalScore = totalPoints;

        string jsonStr = "";
        jsonStr = JsonUtility.ToJson(queManager.data, true);
        Debug.Log(jsonStr);

        // Save the JSON Copy of the Data and Load that in next scene
        // Next Scene will be same in the other checker application

        string jsonPath = Application.persistentDataPath + "userData.json";

        File.WriteAllText(jsonPath, jsonStr);

        FinalMsg();
    }

    public void ShowMsg(string text)
    {
        msg.SetActive(true);
        TextMeshProUGUI txt = msg.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = text;
        StartCoroutine(ClearMsg(txt));
    }

    public void FinalMsg()
    {
        msg.SetActive(true);
        TextMeshProUGUI txt = msg.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = "Game Completed !";
    }

    IEnumerator ClearMsg(TextMeshProUGUI txt)
    {
        yield return new WaitForSeconds(3);
        txt.text = "";
        msg.SetActive(false);
    }

    public void ForAWrongAnswer(int index)
    {
        questionTries[index] = --questionTries[index];
        questionScore[index] = questionScore[index] - decrement;

        Debug.Log(questionTries[index]);
    }

    public void ManageScore(int increment)
    {
        totalPoints += increment;
        scoreui.text = "Points:- " + totalPoints.ToString();
    }
}

[System.Serializable]
public class QuestionData
{
    public Sprite image;
    public int correctAns;
}