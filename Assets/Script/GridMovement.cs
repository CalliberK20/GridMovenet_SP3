using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridMovement : MonoBehaviour
{
    public float health = 100;

    [Space]
    public float speed = 2;
    public float dashForce = 2f;

    Vector3 move;

    public Animator animator;
    public Animator healAnim;
    public SpriteRenderer spriteRenderer;
    [Space, Header("Shooting")]
    public GameObject rotationDirObj;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletDamage;
    public float shootingSpeed = 1;
    [Space]
    public Image healthBar;
    [Space]
    public float recoverTime = 3f;
    public float shieldHealth;
    public GameObject shieldObj;
    [Space]
    public float healthForShield;
    [Space]
    public Animator blinkObj;

    private bool isStagger = false;

    [HideInInspector]
    public bool isSlow = false;

    private bool isShield = false;
    private bool canShield = true;

    private float regSpeed = 0;
    private float slowSpeed = 0;
    private float regHealth = 0;

    private float startTime;
    private List<GameObject> createdBullets = new List<GameObject>();

    private void Start()
    {
        regSpeed = speed;
        regHealth = health;

        for (int i = 0; i < 4; i++)
        {
            createdBullets.Add(Instantiate(bulletPrefab));
            createdBullets[i].transform.parent = GameObject.FindGameObjectWithTag("Pool").transform;
            createdBullets[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStagger)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                animator.SetFloat("MoveX", move.x);
                animator.SetFloat("MoveY", move.y);
                animator.SetBool("Walking", true);
            }
            else
                animator.SetBool("Walking", false);

            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            transform.position += move.normalized * speed * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                blinkObj.transform.position = transform.position;
                blinkObj.SetTrigger("Blink");
                //GetComponent<Rigidbody2D>().position += new Vector2(move.x, move.y).normalized * speed * dashForce * Time.deltaTime;
                GetComponent<Rigidbody2D>().AddForce(new Vector2(move.x, move.y).normalized * 200f);
            }

            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.5f, 9.5f), Mathf.Clamp(transform.position.y, -5.5f, 5f));

            Flip();
        }

        if(isSlow)
        {
            speed = slowSpeed;
        }
        else
        {
            speed = regSpeed;
        }

        if(canShield && !isShield)
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                shieldObj.SetActive(true);
                healthForShield = shieldHealth;
                isShield = true;
                canShield = false;
            }
        }

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - rotationDirObj.transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
        rotationDirObj.transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        if (startTime >= shootingSpeed)
        {
            if (Input.GetMouseButtonDown(0))
                CheckBullet();
        }
        else
            startTime += Time.deltaTime;
    }

    void CheckBullet()
    {
        foreach (GameObject bullet in createdBullets)
        {
            if (!bullet.gameObject.activeSelf)
            {
                bullet.gameObject.SetActive(true);
                bullet.GetComponent<Bullet>().SetBullet(rotationDirObj.transform, bulletSpeed, bulletDamage, 3f, false) ;
                startTime = 0;
                break;
            }
        }
    }

    void Flip()
    {
        if(Input.GetAxisRaw("Horizontal") > 0)
            spriteRenderer.flipX = true;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            spriteRenderer.flipX = false;
    }

    public void Heal(float amount)
    {
        health += amount;
        healAnim.SetTrigger("Heal");
        Debug.Log("Heal");
    }

    public void Damage(float dmg)
    {
        if(isShield)
        {
            healthForShield -= dmg;
            if (healthForShield <= 0)
                StartCoroutine(RecoverShield());
        }
        else
        {
            float conversion = dmg / health;

            healthBar.fillAmount -= conversion;

            health -= dmg;
            animator.SetTrigger("Hurt");
            StartCoroutine(Stagger());
            Debug.Log("Damage");

            if(health <= 0)
            {
                Manager.Instance.ResetCounter();
                health = regHealth;
                healthBar.fillAmount = 1;
            }
        }

        
    }

    public void SetSpeed(float speed)
    {
        Debug.Log("Slow");
        slowSpeed = speed;
        isSlow = true;
    }

    public void SlownessEffect(float speed, float sec)
    {
        StartCoroutine(SlownessEffectEnum(speed, sec));
    }

    private IEnumerator SlownessEffectEnum(float amount, float sec)
    {
        slowSpeed = amount;
        isSlow = true;
        yield return new WaitForSeconds(sec);
        isSlow = false;
    }

    private IEnumerator RecoverShield()
    {
        isShield = false;
        yield return new WaitForSeconds(recoverTime);
        canShield = true;
    }

    private IEnumerator Stagger()
    {
        isStagger = true;
        yield return new WaitForSeconds(0.6f);
        isStagger = false;
    }
}
