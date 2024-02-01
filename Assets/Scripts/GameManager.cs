using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 safePos;

    public TextMeshProUGUI msg;

    [SerializeField]
    public Dictionary<int, string> QuestionDone = new Dictionary<int, string>();

    private Clock clock;

    public TextMeshProUGUI timerui;

    private void Awake()
    {
        if (!clock)
        {
            clock = GameObject.Find("GameClock").GetComponent<Clock>();
        }
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

    public void TouchedLastGate()
    {
        clock.StopClock();
        ShowMsg("Game Completed");

        // Score Validating here

        string result = "";

        foreach(var res in QuestionDone)
        {
            result += "Sr. No:- " + res.Key + " Ans:- " + res.Value + "\n";
        }

        ShowMsg(result);
    }

    public void ShowMsg(string text)
    {
        msg.text = text;
        StartCoroutine(ClearMsg());
    }

    IEnumerator ClearMsg()
    {
        yield return new WaitForSeconds(3);
        msg.text = "";
    }

}