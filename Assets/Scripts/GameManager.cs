using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 safePos;

    // Start is called before the first frame update
    void Start()
    {
        safePos = new Vector3 (0.0f, 0.0f, 0.0f);
    }

    // Change SafePos when we cross the barrier
}
