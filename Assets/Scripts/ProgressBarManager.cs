using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarManager : MonoBehaviour
{
    public Image[] spot; 

    public void ChangeState(int index, bool submitted)
    {
        index--;

        if (submitted)
        {
            spot[index].color = Color.green;
        }
        else
        {
            spot[index].color = Color.red;
        }

    }

    public void QuestionSkipped(int index)
    {
        index--;
        spot[index].color = Color.blue;
    }

}
