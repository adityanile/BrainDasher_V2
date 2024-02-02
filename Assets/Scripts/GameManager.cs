using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 safePos;

    public GameObject msg;

    [SerializeField]
    public Dictionary<int, string> CorrectAnswers = new Dictionary<int, string>();

    public Clock clock;

    public TextMeshProUGUI timerui;
    public TextMeshProUGUI userID;

    [SerializeField]
    private QuestionManager queManager;

    private void Awake()
    {
        if (!clock)
        {
            clock = GameObject.Find("GameClock").GetComponent<Clock>();
        }
        SetCorrectAnswers();
        userID.text = "Player ID:- " + MainManager.mainManager.userID;
    }

    void Start()
    {
        // Start the Clock
        clock.StartClock();
        timerui.text = "Time:- " + clock.DisplayTime();

        ShowMsg("Game Started");

        safePos = new Vector3 (0.0f, 0.0f, 0.0f);
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

        string jsonStr = "";
        jsonStr = JsonUtility.ToJson(queManager.data);
        Debug.Log(jsonStr);

        // Save the JSON Copy of the Data and Load that in next scene
        // Next Scene will be same in the other checker application
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

}