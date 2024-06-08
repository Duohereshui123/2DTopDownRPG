using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour,IDamageable
{
    Rigidbody2D rb;
    Collider2D physicsCollider;

    public int health;

    //health's property
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health <= 0)
            {
                gameObject.BroadcastMessage("OnDie");
                Targetable = false;
            }
            else
            {
                gameObject.BroadcastMessage("OnDamage");
            }
        }
    }
    bool targetable;
    public bool Targetable
    {
        get
        {
            return targetable;
        }
        set
        {
            targetable = value;
            if(!targetable)
            {
                rb.simulated = false;
            }
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }
    public void OnHit(int damage, Vector2 knockBack)
    {
        Health -= damage;
        rb.AddForce(knockBack);
    }
    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }

}
