using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    Slow,
    Damage,
    Heal
}

public class TileDamage : MonoBehaviour
{
    public EffectType zoneType;

    //-------------------------------------
    public float slowSpeed = 0.5f;
    //-------------------------------------
    public float dmgSpeed = 1.0f;
    public float damage = 30f;
    //-------------------------------------
    public float healSpeed = 3f;
    public float heal = 10f;
    
    private bool isCalled = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (zoneType == EffectType.Slow)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<GridMovement>().SetSpeed(slowSpeed);
        }
        else if (zoneType == EffectType.Heal)
        {
            StartCoroutine(Heal(collision));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(zoneType == EffectType.Damage)
        {
            if (!isCalled)
            {
                if (collision.CompareTag("Player"))
                    collision.GetComponent<GridMovement>().StartCoroutine(DamageDelay(collision));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (zoneType == EffectType.Slow)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<GridMovement>().isSlow = false;
        }

        StopAllCoroutines();
    }

    private IEnumerator DamageDelay(Collider2D collision)
    {
        isCalled = true;
        Debug.Log("Trigger");
        collision.GetComponent<GridMovement>().Damage(damage);
        yield return new WaitForSeconds(dmgSpeed);
        isCalled = false;
    }

    private IEnumerator Heal(Collider2D collision)
    {
        while (true)
        {
            yield return new WaitForSeconds(healSpeed);
            collision.GetComponent<GridMovement>().Heal(heal);
        }
    }
}
