using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSplitter : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField]
    private BaseData[] baseData = new BaseData[5];

    public QuestionData GetMeAQuestion(int index)
    {
        int queIndex = Random.Range(0, baseData[index].queData.Length);
        return baseData[index].queData[queIndex];
    }

    [System.Serializable]
    class BaseData
    {
        public QuestionData[] queData = new QuestionData[3];
    }

}
