using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public bool hasKey = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hasKey)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ReceiveKey()
    {
        hasKey = true;
    }
}
