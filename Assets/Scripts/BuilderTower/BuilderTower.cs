using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderTower : MonoBehaviour
{
    [SerializeField] BuilderTowerManager builderTowerManager;
    [SerializeField] Animator buildAnimator;

    public void StartBuildAnim()
    {
        buildAnimator.SetTrigger("Build");
    }

    public void DeactiveBuilder()
    {
        builderTowerManager.DeactiveBuilder();
    }

    public void SetActiveChoosenTower()
    {
        builderTowerManager.SetActiveChoosenTower();
    }

    public void DisableBuilderTowerCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void StopHummerHitSFX()
    {
        GetComponentInParent<BuilderTowerPointer>().DisableSFX();
    }

}
