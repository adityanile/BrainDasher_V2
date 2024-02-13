using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 safePos;

    public GameObject msg;

    [SerializeField]
    public Dictionary<int, string> CorrectAnswers = new Dictionary<int, string>();
    public Dictionary<int, int> questionTries = new Dictionary<int, int>();
    public Dictionary<int, int> questionScore = new Dictionary<int, int>();

    public Clock clock;

    public TextMeshProUGUI timerui;
    public TextMeshProUGUI userID;

    [SerializeField]
    private QuestionManager queManager;

    private int initialPoints = 50;
    private int decrement = 20;

    public TextMeshProUGUI scoreui;
    private int totalPoints = 0;

    private void Awake()
    {
        if (!clock)
        {
            clock = GameObject.Find("GameClock").GetComponent<Clock>();
        }
        SetCorrectAnswers();
        userID.text = "Player ID:- " + MainManager.mainManager.userID;

        InitData();
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

    private void SetCorrectAnswers()
    {
        CorrectAnswers[1] = "a";
        CorrectAnswers[2] = "b";
        CorrectAnswers[3] = "c";
        CorrectAnswers[4] = "d";
        CorrectAnswers[5] = "1";
    }

    public void TouchedLastGate()
    {
        clock.StopClock();
        ShowMsg("Game Completed");

        queManager.data.gameCompletionTime = clock.GetCurrectTimeJSON();
        queManager.data.totalScore = totalPoints;

        string jsonStr = "";
        jsonStr = JsonUtility.ToJson(queManager.data, true);
        Debug.Log(jsonStr);

        // Save the JSON Copy of the Data and Load that in next scene
        // Next Scene will be same in the other checker application

        string jsonPath = Application.persistentDataPath + "userData.json";

        File.WriteAllText(jsonPath, jsonStr);
        
    }

    public void ShowMsg(string text)
    {
        msg.SetActive(true);
        TextMeshProUGUI txt = msg.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = text;
        StartCoroutine(ClearMsg(txt));
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