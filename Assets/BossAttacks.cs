using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAttacks : MonoBehaviour
{
    public float health = 40;
    public float moveSpeed = 1f;
    public Image healthBar;
    [Header("MELEE")]
    public GameObject zoneObj;
    public float damage;

    [Header("Range Attack")]
    public bool isRange = false;
    public GameObject bulletprefab;
    [Space]
    public int bulletCount = 5;
    public float castingSpeed = 3f;
    public Transform target;

    //-----------PRIVATE--------------------
    private Animator animator;
    private float regFlip = 0;
    private bool beginningAttack = false;
    private bool isCalled = false;
    private List<GameObject> bullets = new List<GameObject>();
    private float regHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        regFlip = transform.localScale.x;

        if(isRange)
        {
            for (int i = 0; i < bulletCount * 2; i++)
            {
                bullets.Add(Instantiate(bulletprefab));
                bullets[i].transform.parent = GameObject.FindGameObjectWithTag("Pool").transform;
                bullets[i].SetActive(false);
            }
        }

        regHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (!beginningAttack)
        {
            if (Vector3.Distance(transform.position, target.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                animator.SetBool("Move", true);
                FlipOnMove();
            }
            else
            {
                animator.SetBool("Move", false);
            }
            if(!isCalled)
            StartCoroutine(Attack());
        }
    }

    private void FlipOnMove()
    {
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector3(regFlip, transform.localScale.y);
            healthBar.rectTransform.localScale = new Vector3(regFlip, transform.localScale.y);
        }
        else if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(-regFlip, transform.localScale.y);
            healthBar.rectTransform.localScale = new Vector3(-regFlip, transform.localScale.y);
        }
    }

    private IEnumerator Attack()
    {
        isCalled = true;
        yield return new WaitForSeconds(castingSpeed);
        beginningAttack = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
    }

    public void Damage(float damage)
    {
        float conversion = damage / regHealth;
        healthBar.fillAmount -= conversion;
        health -= damage;

        if(health <= 0)
        {
            animator.SetTrigger("Die");
            GetComponent<Collider2D>().enabled = false;
            StopAllCoroutines();
            enabled = false;
        }
    }

    public IEnumerator AttackAnim()
    {
        if(isRange)
        {
            float speed = 1f;

            for (int i = 0; i < (bulletCount * 2) / 2; i++)
            {
                float rotation = 90f * i;

                bullets[i].SetActive(true);
                bullets[i].transform.position = transform.position;
                bullets[i].transform.rotation = Quaternion.Euler(0f, 0f, rotation / 2f);
                bullets[i].GetComponent<Bullet>().SetBullet(bullets[i].transform, speed, 2f, 6f, true);
            }
            yield return new WaitForSeconds(2f);

            for (int i = (bulletCount * 2) / 2; i < (bulletCount * 2); i++)
            {
                float rotation = 90f * i;

                bullets[i].SetActive(true);
                bullets[i].transform.position = transform.position;
                bullets[i].transform.rotation = Quaternion.Euler(0f, 0f, (rotation / 2f) + 30);
                bullets[i].GetComponent<Bullet>().SetBullet(bullets[i].transform, speed, 2f, 6f, true);
            }
        }
        else
        {
            yield return new WaitForSeconds(2f);
            Collider2D hit = Physics2D.OverlapBox(zoneObj.transform.position, zoneObj.transform.localScale, 0);
            if(hit != null)
            {
                if(hit.CompareTag("Player"))
                {
                    Debug.Log("HIT PLAYER");
                }
            }
        }

        beginningAttack = false;
    }

    public void StartMoving()
    {
        isCalled = false;
    }
}
