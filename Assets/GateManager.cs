using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    public bool hasKey = false;

    public List<GameObject> enemiesToDefeat = new List<GameObject>();

    private void OnEnable()
    {
        foreach(GameObject enemy in enemiesToDefeat)
        {
            GiveKey enemyKey = enemy.AddComponent<GiveKey>();
            enemyKey.parentGateManager = this;
        }
    }

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

    public void RemoveFromChildAndDesipate(GameObject childToRemove)
    {
        enemiesToDefeat.Remove(childToRemove);

        if (enemiesToDefeat.Count <= 0)
            gameObject.SetActive(false);
    }

    public void ReceiveKey()
    {
        hasKey = true;
    }
}
