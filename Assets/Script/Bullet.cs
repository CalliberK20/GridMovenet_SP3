using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Rigidbody2D rb;
    private float life;
    private bool isEnemy = false;


    public void SetBullet(Transform direction, float speed, float damage, float life, bool isEnemy)
    {
        StopAllCoroutines();
        transform.position = direction.position + new Vector3(0, 0.3f);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.up * speed;
        this.damage = damage;
        this.life = life;
        this.isEnemy = isEnemy;
        StartCoroutine(Desipate());
    }

    private IEnumerator Desipate()
    {
        yield return new WaitForSeconds(life);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isEnemy)
        {
            if(collision.CompareTag("Player"))
            {
                collision.GetComponent<GridMovement>().Damage(damage);
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<FollowTarget>().Damage(damage);
                gameObject.SetActive(false);
            }
            else if(collision.CompareTag("Boss"))
            {
                collision.GetComponent<BossAttacks>().Damage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}
