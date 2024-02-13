using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager mainManager;

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

    public void OnClickSubmit()
    {
        userID = id.text;
        SceneManager.LoadScene(2);
    }

}
