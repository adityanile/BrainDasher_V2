using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasswordManager : MonoBehaviour
{
    public TMP_InputField Inputpass;
    public TextMeshProUGUI msg;

    private string pass = "170224_BD";

    public void OnClickEnter()
    {
        if(Inputpass.text == pass)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            msg.text = "Invalid Credentials";
        }
    }
}
