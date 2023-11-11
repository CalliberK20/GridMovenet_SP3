using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float health = 10;
    public float speed = 5;
    public float atkDamage = 3;
    public float atkSpeed;
    public Transform target;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    //private bool isDancing = false;

    private float regFlip = 0;

    private bool isAttacking = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        regFlip = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            animator.SetBool("Move", true);
            FlipOnMove();

/*            if(!isDancing)
            {
            }*/
        }
        else
        {
            animator.SetBool("Move", false);

            if (!isAttacking)
                StartCoroutine(Attack());
/*            Debug.Log("Hit");
            if (!isDancing)
                StartCoroutine(Dance());*/
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(atkSpeed);
        isAttacking = false;
    }

    public void Damage(float dmg)
    {
        health -= dmg;
    }

    public void SetSpeed(float speed)
    {
        this.speed += speed;
    }

    private void FlipOnMove()
    {
        if (transform.position.x > target.position.x)
        {
            /*animator.SetFloat("MoveX", 1);*/
            transform.localScale = new Vector3(regFlip, transform.localScale.y);
        }
        else if (transform.position.x < target.position.x)
        {
            /*animator.SetFloat("MoveX", -1);*/
            transform.localScale = new Vector3(-regFlip, transform.localScale.y);
        }
/*        else if (transform.position.y > target.position.y)
            animator.SetFloat("MoveY", 1);
        else if (transform.position.y < target.position.y)
            animator.SetFloat("MoveY", -1);*/
    }
/*
    private IEnumerator Dance()
    {
        isDancing = true;

        Manager.Instance.ResetCounter();
        speed = 1;

        animator.SetTrigger("Dance");
        yield return new WaitForSeconds(danceSec);

        isDancing = false;
    }*/
}