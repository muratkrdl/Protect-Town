using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] float setColliderRadiusValue = .3f;

    void Start() 
    {
        Destroy(gameObject,.33f);
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
        StartCoroutine(IncreaseColliderRadius());
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator IncreaseColliderRadius()
    {
        GetComponent<CircleCollider2D>().radius = setColliderRadiusValue;
        yield return new WaitForSeconds(.1f);
        GetComponent<Collider2D>().enabled = false;
        StopCoroutine(IncreaseColliderRadius());
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }    
    }

    public void KYS()
    {
        Destroy(gameObject);
    }

}
