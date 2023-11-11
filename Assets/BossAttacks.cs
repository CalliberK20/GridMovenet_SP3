using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    public GameObject bulletprefab;
    public Animator animator;
    [Space]
    public float moveSpeed = 1f;
    [Space]
    public int bulletCount = 5;
    public float castingSpeed = 3f;
    public Transform target;

    private float regFlip = 0;
    private bool beginningAttack = false;
    private bool isCalled = false;

    private List<GameObject> bullets = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        regFlip = transform.localScale.x;

        for (int i = 0; i < bulletCount * 2; i++)
        {
            bullets.Add(Instantiate(bulletprefab));
            bullets[i].SetActive(false);
        }
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
        }
        else if (transform.position.x < target.position.x)
        {
            transform.localScale = new Vector3(-regFlip, transform.localScale.y);
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

    public IEnumerator AttackAnim()
    {
        float speed = 1f;

        for (int i = 0; i < (bulletCount * 2) / 2; i++)
        {
            float rotation = 90f * i;

            bullets[i].SetActive(true);
            bullets[i].transform.position = transform.position;
            bullets[i].transform.rotation = Quaternion.Euler(0f, 0f, rotation / 2f);
            bullets[i].GetComponent<Bullet>().SetBullet(bullets[i].transform, speed, 2f, 6f);
        }
        yield return new WaitForSeconds(2f);

        for (int i = (bulletCount * 2) / 2; i < (bulletCount * 2); i++)
        {
            float rotation = 90f * i;

            bullets[i].SetActive(true);
            bullets[i].transform.position = transform.position;
            bullets[i].transform.rotation = Quaternion.Euler(0f, 0f, (rotation / 2f) + 30);
            bullets[i].GetComponent<Bullet>().SetBullet(bullets[i].transform, speed, 2f, 6f);
        }

        beginningAttack = false;
        
    }

    public void StartMoving()
    {
        isCalled = false;
    }
}
