using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager mainManager;

    public GameObject initialUi;
    public GameObject storyUi;
    public GameObject insturctionUI;
    public GameObject descriptionUI;

    public TMP_InputField id;

    public string userID;

    void Awake()
    {
        if (mainManager)
        {
            Destroy(gameObject);
        }

        mainManager = this;
        DontDestroyOnLoad(gameObject);
    }


    public void ShowStory()
    {
        initialUi.SetActive(false);
        storyUi.SetActive(true);
    }

    public void ShowInstructions()
    {
        storyUi.SetActive(false);
        insturctionUI.SetActive(true);
    }

    public void ShowDecription()
    {
        insturctionUI.SetActive(false);
        descriptionUI.SetActive(true);
    }

    public void BackFromInstruction()
    {
        storyUi.SetActive(true);
        insturctionUI.SetActive(false);  
    }

    public void OnClickSubmit()
    {
        userID = id.text;
        SceneManager.LoadScene(2);
    }

}
