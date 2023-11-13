using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfernoTower : MonoBehaviour
{
    [Header("FireBall")]
    [SerializeField] GameObject fireBallPrefab;

    [Header("Level Up")]
    [SerializeField] int costLevel2;
    [SerializeField] int costLevel3;
    [SerializeField] TextMeshProUGUI levelUpCostText;

    [Header("FireBall Damage")]
    [SerializeField] int damageLevel1;
    [SerializeField] int damageLevel2;
    [SerializeField] int damageLevel3;

    [Header("InfernoAnimationBoolName")]
    [SerializeField] string level1AnimName;
    [SerializeField] string level2AnimName;
    [SerializeField] string level3AnimName;

    [Header("Variable")]
    [SerializeField] float chaseRange;
    [SerializeField] Transform[] fireBallOutPut;
    [SerializeField] Animator animator;

    [Header("GamePlay")]
    [SerializeField] float fireBallSpeed = 50;
    [SerializeField] float fireRate;

    string level2 = "Level2";
    string level3 = "Level3";
    string currentLevel;

    Transform fireBallParent;
    BuilderTowerPointer builderTowerPointer;
    GameObject currentFireBall;
    int currentDamage;
    string currentLevelAnimName;
    Vector2 fireBallOutPutDirection;
    Enemy chosenEnemy;
    bool animating = false;
    float currentZRotation;

    void Awake()
    {
        currentLevel = "Level1";
        levelUpCostText.text = costLevel2.ToString();
        builderTowerPointer = GetComponentInParent<BuilderTowerPointer>();
        currentFireBall = fireBallPrefab;
        currentLevelAnimName = level1AnimName;
        currentDamage = damageLevel1;
        fireBallParent = GameObject.FindGameObjectWithTag("FIREBALLS").transform;
    }

    void Update() 
    {
        FindClosestTarget();
        if(!animating) 
            LookAtEnemy();
        if(!animating && !GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = true;
        if(animating && GetComponent<Collider2D>().enabled)
            GetComponent<Collider2D>().enabled = false;
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
            if(targetDistance <= maxDistance && enemy.GetComponent<Collider2D>())
            {
                closestTarget = enemy;
                maxDistance = targetDistance;
                Attack(true);
            }
        }

        chosenEnemy = closestTarget;
    }

    void LookAtEnemy()
    {
        if(chosenEnemy == null) { Attack(false); return; }
        float targetDistance = Vector2.Distance(GetComponentInParent<Transform>().position,chosenEnemy.transform.position);

        Vector3 look = transform.InverseTransformPoint(chosenEnemy.transform.position);
        float angle = Mathf.Atan2(look.y,look.x) * Mathf.Rad2Deg -90;
        transform.Rotate(0,0,angle);
        currentZRotation = transform.localRotation.eulerAngles.z;
    }

    void Attack(bool isActive)
    {
        if(animator.GetBool(currentLevelAnimName) == isActive) { return; }

        animator.SetBool(currentLevelAnimName,isActive);
    }

    public void AttackForAnimEventFirstBall()
    {
        AttackForAnimEventInstantiate(fireBallOutPut[0],0);
    }

    public void AttackForAnimEventSecondBall()
    {
        AttackForAnimEventInstantiate(fireBallOutPut[1],180);
    }

    public void AttackForAnimEventThirdBall()
    {
        AttackForAnimEventInstantiate(fireBallOutPut[2],270);
    }

    public void AttackForAnimEventFourthBall()
    {
        AttackForAnimEventInstantiate(fireBallOutPut[3],90);
    }

    void AttackForAnimEventInstantiate(Transform fireBallOutPut,float rotation)
    {
        if(!fireBallOutPut.gameObject.activeSelf) { return; }

        fireBallOutPutDirection = (fireBallOutPut.position - transform.position).normalized;
        var fireBall = Instantiate(fireBallPrefab,fireBallOutPut.position,transform.rotation,fireBallParent);
        fireBall.transform.rotation = Quaternion.Euler(0,0,currentZRotation + rotation);
        fireBall.GetComponent<FireBall>().Damage = currentDamage;
        fireBall.GetComponent<Rigidbody2D>().AddForce(fireBallOutPutDirection * fireBallSpeed);
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
            animator.SetTrigger(level3);
            builderTowerPointer.ChosedSomething();
            builderTowerPointer.enabled = false;
        }
    }

    public void SetRotationZeroForAnimation()
    {
        animating = true;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void FreeRotationForAnimation()
    {
        animating = false;
    }

}
