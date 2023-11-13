using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float dieVolume;

    [SerializeField] Animator animator;
    [SerializeField] int awardAmount;
    [SerializeField] float teslaSlowAmount;
    [SerializeField] int maxHitPoints;
    [SerializeField] Enemy enemy;

    [SerializeField] AudioClip[] dieSFX;

    int currentHitPoints;

    public void SetHitPoints()
    {
        currentHitPoints = maxHitPoints;
    }

    void DecreaseHitPoints(int damage)
    {
        currentHitPoints -= damage;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Arrow"))
        {
            var arrow = other.gameObject.GetComponent<Arrow>();
            if(arrow == null) { return; }
            arrow.HitSomething();
            DecreaseHitPoints(arrow.Damage);
            arrow.transform.position = transform.position;
            CheckDie();
        }
        else if(other.CompareTag("Projectile"))
        {
            var projectile = other.gameObject.GetComponent<Projectile>();
            if(projectile == null) { return; }
            DecreaseHitPoints(projectile.Damage);
            StartCoroutine(SlowbyTesla());
            CheckDie();
        }
        else if(other.CompareTag("FireBall"))
        {
            var fireBall = other.gameObject.GetComponent<FireBall>();
            if(fireBall == null) { return; }
            DecreaseHitPoints(fireBall.Damage);
            fireBall.HitSomething();
            fireBall.transform.position = transform.position;
            CheckDie();
        }
    }

    IEnumerator SlowbyTesla()
    {
        enemy.SetMoveSpeed = 0;
        enemy.GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(teslaSlowAmount);
        enemy.GetComponent<Animator>().enabled = true;
        enemy.SetMoveSpeedToInitial();
        StopCoroutine(SlowbyTesla());
    }

    void CheckDie()
    {
        if(currentHitPoints <= 0)
        {
            //Die
            SoundManager.volumeAmount = dieVolume;
            SoundManager.PlaySound(dieSFX[Random.Range(0,dieSFX.Length)]);
            FindObjectOfType<Bank>().IncreaseMoney(awardAmount);
            animator.SetTrigger("Die");
            GetComponent<Collider2D>().enabled = false;
            enemy.enabled = false;
            enemy.Stop();
            gameObject.SetActive(false);
        }
    }

}
