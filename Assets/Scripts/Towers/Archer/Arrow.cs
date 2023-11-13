using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Start() 
    {
        Destroy(gameObject,10);
    }

    int damage;

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    public void HitSomething()
    {
        animator.SetTrigger("Hit");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void KYS()
    {
        Destroy(gameObject);
    }
    
}
