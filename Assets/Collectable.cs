using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Sprite[] spriteItems;
    [Space]
    public EffectType effectType;

    public float heal = 10f;
    public float damage = 10f;

    public float slownessTime = 2f;
    public float slowness = 0.4f;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        switch (effectType)
        {
            case EffectType.Slow:
                spriteRenderer.sprite = spriteItems[0];
                break;
            case EffectType.Damage:
                spriteRenderer.sprite = spriteItems[1];
                break;
            case EffectType.Heal:
                spriteRenderer.sprite = spriteItems[2];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GridMovement player = collision.GetComponent<GridMovement>();

            switch (effectType)
            {
                case EffectType.Slow:
                    player.SlownessEffect(slowness, slownessTime);
                    break;
                case EffectType.Damage:
                    player.Damage(damage);
                    break;
                case EffectType.Heal:
                    player.Heal(heal);
                    break;
            }

            gameObject.SetActive(false);
        }
    }
}
