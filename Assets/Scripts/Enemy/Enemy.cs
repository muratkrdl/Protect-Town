using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] float moveSpeed;
    [SerializeField] int damageAmount;

    [Header("Point Names")]
    [SerializeField] string goUpPointName;
    [SerializeField] string goDownPointName;
    [SerializeField] string goRightPointName;
    [SerializeField] string goLeftPointName;
    
    [SerializeField] Animator animator;

    PathController pathController;

    float initialMoveSpeed;

    void OnEnable() 
    {
        Enable();
        SetMoveSpeedToInitial();
        animator.enabled = true;
    }

    public float SetMoveSpeed
    {
        set
        {
            moveSpeed = value;
        }
    }

    public void SetMoveSpeedToInitial()
    {
        moveSpeed = initialMoveSpeed;
    }

    public void Move() 
    {
        StartCoroutine(MoveToBase());
    }

    public void Stop()
    {
        StopCoroutine(MoveToBase());
    }

    void Awake()
    {
        initialMoveSpeed = moveSpeed;    
        animator = GetComponent<Animator>();
        pathController = GameObject.FindGameObjectWithTag("PathController").GetComponent<PathController>();
    }

    IEnumerator MoveToBase()
    {
        foreach(Transform pathPoint in pathController.GetPath)
        {
            if(pathPoint == null) { break; }
            Vector3 startPosition = transform.position;
            Vector3 endPosition = pathPoint.position;
            float lerpPercent = 0f;
            if(gameObject.name == "Werewolf(Clone)") { endPosition.y += .3f; }

            if(pathPoint.name == goDownPointName)
                GoDown();
            else if(pathPoint.name == goUpPointName)
                GoUp();
            else if(pathPoint.name == goRightPointName)
                GoRight();
            else if(pathPoint.name == goLeftPointName)
                GoLeft();
                       
            while(lerpPercent < 1)
            {
                lerpPercent += Time.deltaTime * moveSpeed;
                transform.position = Vector2.Lerp(startPosition, endPosition, lerpPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Finish"))
        {
            //Give damage to player
            FindObjectOfType<BaseHealth>().DecreaseHitPoints(damageAmount);
            DisableEnemy();
        }
    }

    void Enable()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<EnemyHealth>().enabled = true;
        GetComponent<Enemy>().enabled = true;
    }

    void DisableEnemy()
    {
        gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyHealth>().enabled = false;
    }

    #region AnimationFunction
    void GoUp()
    {
        animator.SetTrigger("U");
    }

    void GoDown()
    {
        animator.SetTrigger("D");
    }

    void GoRight()
    {
        animator.SetTrigger("S");
        if(GetComponent<SpriteRenderer>().flipX != true)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    void GoLeft()
    {
        animator.SetTrigger("S");
        if(GetComponent<SpriteRenderer>().flipX != false)
            GetComponent<SpriteRenderer>().flipX = false;
    }
    #endregion

}
