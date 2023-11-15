using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowTarget : MonoBehaviour
{
    public float searchRadius = 10;
    [Space]
    public Image healthBar;
    [Space]
    public float health = 10;
    public float speed = 5;
    public float atkDamage = 3;
    public float atkSpeed;
    public Transform target;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public SpriteRenderer[] hurtSprites;

    //private bool isDancing = false;

    private float regFlip = 0;

    private bool isAttacking = false;
    private float regHealth;

    private AudioSource audioSource;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        //spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        regFlip = transform.localScale.x;
        regHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.position) < searchRadius && !Manager.Instance.inConversation)
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
    }

    private IEnumerator Attack()
    {
        audioSource.Play();
        isAttacking = true;
        animator.SetTrigger("Attack");
        target.GetComponent<GridMovement>().Damage(atkDamage);
        yield return new WaitForSeconds(atkSpeed);
        isAttacking = false;
    }

    private IEnumerator Hurt()
    {
        foreach (SpriteRenderer sprite in hurtSprites)
        {
            sprite.color = Color.red;
        }
        yield return new WaitForSeconds(Time.deltaTime);
        foreach(SpriteRenderer sprite in hurtSprites)
        {
            sprite.color = Color.white;
        }
        yield return new WaitForSeconds(Time.deltaTime);
        foreach (SpriteRenderer sprite in hurtSprites)
        {
            sprite.color = Color.red;
        }
        yield return new WaitForSeconds(Time.deltaTime);
        foreach (SpriteRenderer sprite in hurtSprites)
        {
            sprite.color = Color.white;
        }
    }

    public void Damage(float dmg)
    {
        float conversion = dmg / regHealth;
        healthBar.fillAmount -= conversion;
        health -= dmg;
        StartCoroutine(Hurt());

        if(health <= 0)
        {
            StartCoroutine(SetDisableDelay());
        }
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

    private IEnumerator SetDisableDelay()
    {
        animator.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
