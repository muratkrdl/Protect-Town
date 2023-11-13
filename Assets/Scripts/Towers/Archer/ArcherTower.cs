using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    [Header("Arrow")]
    [SerializeField] GameObject arrowLevel1Prefab;
    [SerializeField] GameObject arrowLevel2Prefab;
    [SerializeField] GameObject arrowLevel3Prefab;

    [Header("Level Up")]
    [SerializeField] int costLevel2;
    [SerializeField] int costLevel3;
    [SerializeField] TextMeshProUGUI levelUpCostText;

    [Header("Arrow Damage")]
    [SerializeField] int damageLevel1;
    [SerializeField] int damageLevel2;
    [SerializeField] int damageLevel3;

    [Header("BowAnimationBoolName")]
    [SerializeField] string level1AnimName;
    [SerializeField] string level2AnimName;
    [SerializeField] string level3AnimName;

    [Header("Variable")]
    [SerializeField] float chaseRange;
    [SerializeField] Transform arrowOutPut;
    [SerializeField] GameObject archerWeapon;
    [SerializeField] Animator bowAnimator;

    [Header("GamePlay")]
    [SerializeField] float arrowSpeed = 50;
    [SerializeField] float fireRateLevel1;
    [SerializeField] float fireRateLevel2;
    [SerializeField] float fireRateLevel3;

    string level2 = "Level2";
    string level3 = "Level3";
    string currentLevel;

    Transform arrowsParent;
    Vector2 arrowOutPutDirection;
    Enemy chosenEnemy;
    BuilderTowerPointer builderTowerPointer;
    Animator towerAnimator;
    GameObject currentArrow;
    int currentDamage;
    string currentLevelAnimName;
    float currentFireRate;

    void Awake()
    {
        currentFireRate = fireRateLevel1;
        builderTowerPointer = GetComponentInParent<BuilderTowerPointer>();
        levelUpCostText.text = costLevel2.ToString();
        currentLevel = "Level1";
        currentArrow = arrowLevel1Prefab;
        currentLevelAnimName = level1AnimName;
        currentDamage = damageLevel1;
        towerAnimator = GetComponent<Animator>();
        arrowsParent = GameObject.FindGameObjectWithTag("ARROWS").transform;
    }

    void Update() 
    {
        LookAtEnemy();
        FindClosestTarget();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy closestTarget = null;
        float maxDistance = chaseRange;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector2.Distance(archerWeapon.transform.position,enemy.transform.position);
            if(enemy == null) { chosenEnemy = null; }
            if(targetDistance <= maxDistance && enemy.GetComponent<Collider2D>())
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
            }
        }

        chosenEnemy = closestTarget;
    }

    void LookAtEnemy()
    {
        if(chosenEnemy == null) { Attack(false); return; }

        float targetDistance = Vector2.Distance(GetComponentInParent<Transform>().position,chosenEnemy.transform.position);

        Vector2 look = archerWeapon.transform.InverseTransformPoint(chosenEnemy.transform.position);
        float angle = Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg -90;
        archerWeapon.transform.Rotate(0,0,angle);
        Attack(true);
        arrowOutPutDirection = (chosenEnemy.transform.position - archerWeapon.transform.position).normalized;
    }

    void Attack(bool isActive)
    {
        if(bowAnimator.GetBool(currentLevelAnimName) == isActive) { return; }

        bowAnimator.SetBool(currentLevelAnimName,isActive);
    }

    public void AttackForAnimEvent()
    {
        var arrow = Instantiate(currentArrow,arrowOutPut.position,archerWeapon.transform.rotation,arrowsParent);
        arrow.GetComponent<Arrow>().Damage = currentDamage;
        arrow.GetComponent<Rigidbody2D>().AddForce(arrowOutPutDirection * arrowSpeed);
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
            currentArrow = arrowLevel2Prefab;
            currentLevelAnimName = level2AnimName;
            currentDamage = damageLevel2;
            towerAnimator.SetTrigger(level2);
            levelUpCostText.text = costLevel3.ToString();
            builderTowerPointer.ChosedSomething();
            currentFireRate = fireRateLevel2;
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
            currentArrow = arrowLevel3Prefab;
            currentLevelAnimName = level3AnimName;
            currentDamage = damageLevel3;
            towerAnimator.SetTrigger(level3);
            builderTowerPointer.ChosedSomething();
            currentFireRate = fireRateLevel3;
            builderTowerPointer.enabled = false;
        }
    }

    public void DisableBowForLevelUp()
    {
        bowAnimator.gameObject.SetActive(false);
    }

    public void EnableBowForLevelUp()
    {
        bowAnimator.gameObject.SetActive(true);
        bowAnimator.SetTrigger(currentLevel);
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

}
