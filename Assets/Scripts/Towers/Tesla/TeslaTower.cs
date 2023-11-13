using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeslaTower : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] GameObject projectileLevel1Prefab;
    [SerializeField] GameObject projectileLevel2Prefab;
    [SerializeField] GameObject projectileLevel3Prefab;

    [Header("Level Up")]
    [SerializeField] int costLevel2;
    [SerializeField] int costLevel3;
    [SerializeField] TextMeshProUGUI levelUpCostText;

    [Header("Projectile Damage")]
    [SerializeField] int damageLevel1;
    [SerializeField] int damageLevel2;
    [SerializeField] int damageLevel3;

    [Header("TeslaAnimationBoolName")]
    [SerializeField] string level1AnimName;
    [SerializeField] string level2AnimName;
    [SerializeField] string level3AnimName;

    [Header("Variable")]
    [SerializeField] float chaseRange;
    [SerializeField] Animator animator;

    [Header("GamePlay")]
    [SerializeField] float fireRate;

    string level2 = "Level2";
    string level3 = "Level3";
    string currentLevel;

    Transform projectilesParent;
    BuilderTowerPointer builderTowerPointer;
    Enemy chosenEnemy;
    GameObject currentProjectile;
    int currentDamage;
    string currentLevelAnimName;

    void Awake() 
    {
        currentLevel = "Level1";
        levelUpCostText.text = costLevel2.ToString();
        builderTowerPointer = GetComponentInParent<BuilderTowerPointer>();
        currentProjectile = projectileLevel1Prefab;
        currentLevelAnimName = level1AnimName;
        currentDamage = damageLevel1;
        projectilesParent = GameObject.FindGameObjectWithTag("PROJECTILES").transform;
    }

    void Update() 
    {
        FindClosestTarget();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closestTarget = null;
        float maxDistance = chaseRange;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector2.Distance(transform.position,enemy.transform.position);
            if(enemy == null) { chosenEnemy = null; }
            if(targetDistance <= maxDistance)
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
                Attack(true);
            }
        }

        chosenEnemy = closestTarget;
    }

    void Attack(bool isActive)
    {
        if(animator.GetBool(currentLevelAnimName) == isActive) { return; }

        animator.SetBool(currentLevelAnimName,isActive);
    }

    public void InstantiateProjectile()
    {
        if(chosenEnemy == null) { return; }
        var projectile = Instantiate(currentProjectile,transform.position,Quaternion.identity,projectilesParent);
        projectile.transform.position = chosenEnemy.transform.position;
    }

    public void LevelUp()
    {
        if(currentLevel == "Level1")
        {
            if(FindObjectOfType<Bank>().GetCurrentMoney < costLevel2)
            { 
                builderTowerPointer.ChosedSomething();
                return; 
            }
            //Level Up 2
            FindObjectOfType<Bank>().DecreaseMoney(costLevel2);
            currentLevel = level2;
            currentDamage = damageLevel2;
            currentLevelAnimName = level2AnimName;
            currentProjectile = projectileLevel2Prefab;
            levelUpCostText.text = costLevel3.ToString();
            animator.SetTrigger(level2);
            builderTowerPointer.ChosedSomething();
        }
        else if(currentLevel == level2)
        {
            if(FindObjectOfType<Bank>().GetCurrentMoney < costLevel3) 
            { 
                builderTowerPointer.ChosedSomething();
                return; 
            }
            //Level Up 3
            FindObjectOfType<Bank>().DecreaseMoney(costLevel3);
            currentLevel = level3;
            currentDamage = damageLevel3;
            currentLevelAnimName = level3AnimName;
            currentProjectile = projectileLevel3Prefab;
            animator.SetTrigger(level3);
            builderTowerPointer.ChosedSomething();
            builderTowerPointer.enabled = false;
        }
    }

    public void DisableCollider()
    {
        if(GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = false;
    }

    public void EnableCollider()
    {
        if(!GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = true;
    }

    public void EnableAttackForAnim()
    {
        Attack(false);
    }

}
