using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public float health = 10;
    public float speed = 5;
    public float danceSec = 3;
    public Transform target;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isDancing = false;

    private void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            if(!isDancing)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                FlipOnMove();
            }
        }
        else
        {
            Debug.Log("Hit");
            if (!isDancing)
                StartCoroutine(Dance());
        }
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
            animator.SetFloat("MoveX", 1);
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x < target.position.x)
        {
            animator.SetFloat("MoveX", -1);
            spriteRenderer.flipX = true;
        }
        else if (transform.position.y > target.position.y)
            animator.SetFloat("MoveY", 1);
        else if (transform.position.y < target.position.y)
            animator.SetFloat("MoveY", -1);
    }

    private IEnumerator Dance()
    {
        isDancing = true;

        Manager.Instance.ResetCounter();
        speed = 1;

        animator.SetTrigger("Dance");
        yield return new WaitForSeconds(danceSec);

        isDancing = false;
    }
}
