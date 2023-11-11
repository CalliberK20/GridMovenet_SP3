using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Rigidbody2D rb;
    private float life;

    public void SetBullet(Transform direction, float speed, float damage, float life)
    {
        StopAllCoroutines();
        transform.position = direction.position + new Vector3(0, 0.3f);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.up * speed;
        this.damage = damage;
        this.life = life;
        StartCoroutine(Desipate());
    }

    private IEnumerator Desipate()
    {
        yield return new WaitForSeconds(life);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<FollowTarget>().Damage(damage);
            gameObject.SetActive(false);
        }
    }
}
