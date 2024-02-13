using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManage : MonoBehaviour
{
    public GameObject node;
    public float speed = 1f;

    public Vector3 movePos;

    private void Awake()
    {
        node = GameObject.Find("Node");
    }


    // Update is called once per frame
    void Update()
    {
        movePos = (node.transform.position - gameObject.transform.position).normalized;

        if (movePos != Vector3.zero)
        {
            transform.Translate(movePos * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Node"))
        {
            Destroy(gameObject);
        }

    }

}
