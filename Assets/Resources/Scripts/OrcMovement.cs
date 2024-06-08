using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    DetectionZone detectionZone;
    Animator animator;

    public float speed;
    public float knockBackForce;
    public int attackPower;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        detectionZone = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs != null)
        {
            Vector2 direction = (detectionZone.detectedObjs.transform.position - transform.position);
            if (direction.magnitude <= detectionZone.viewRadius)
            {
                rb.AddForce(direction.normalized * speed); //归一化
                //rb.AddForce(direction * speed);
                if(direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                if (direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                OnWalk();
            }
            else
            {
                OnWalkStop();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null && collider.tag == "Player")
        {
            Vector2 direction = collider.transform.position - transform.position;
            Vector2 force = direction.normalized * knockBackForce;
            damageable.OnHit(attackPower,force);
        }
    }
    public void OnWalk()
    {
        animator.SetBool("isWalking", true);
    }
    public void OnWalkStop()
    {
        animator.SetBool("isWalking", false);
    }

    public void OnDamage()
    {
        animator.SetTrigger("isDamage");
    }
    public void OnDie()
    {
        animator.SetTrigger("isDie");
    }

}
