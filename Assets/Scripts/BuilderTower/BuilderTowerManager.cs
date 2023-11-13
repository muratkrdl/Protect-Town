using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Archer,
    Tesla,
    Infernal
}

public class BuilderTowerManager : MonoBehaviour
{
    [Header("Sprite")]

    [SerializeField] Sprite archerTowerSprite;
    [SerializeField] Sprite teslaTowerSprite;
    [SerializeField] Sprite infernalTowerSprite;

    [Header("Children")]

    [SerializeField] BuilderTower builderTower;
    [SerializeField] GameObject archerGameObject;
    [SerializeField] GameObject teslaGameObject;
    [SerializeField] GameObject infernoGameObject;

    TowerType chosenTowerType;

    Sprite chosenTowerSprite;

    void Awake() 
    {
        if(!builderTower.gameObject.activeSelf)
            builderTower.gameObject.SetActive(true);
        if(archerGameObject.activeSelf)
            archerGameObject.SetActive(false);
        if(teslaGameObject.activeSelf)
            teslaGameObject.SetActive(false);
        if(infernoGameObject.activeSelf)
            infernoGameObject.SetActive(false);
    }


    #region ANİM_EVENT_REGİON
    public void DeactiveBuilder()
    {
        builderTower.gameObject.SetActive(false);
    }

    public void SetActiveChoosenTower()
    {
        switch (chosenTowerType)
        {
            case TowerType.Archer:
                chosenTowerSprite = archerTowerSprite;
                break;
            case TowerType.Tesla:
                chosenTowerSprite = teslaTowerSprite;
                break;
            case TowerType.Infernal:
                chosenTowerSprite = infernalTowerSprite;
                break;
            default:
                chosenTowerSprite = null;
                break;
        }

        if(chosenTowerSprite == archerTowerSprite)
            archerGameObject.SetActive(true);
        else if(chosenTowerSprite == teslaTowerSprite)
            teslaGameObject.SetActive(true);
        else if(chosenTowerSprite == infernalTowerSprite)
            infernoGameObject.SetActive(true);
    }
    #endregion

    public void ChoseArcher()
    {
        chosenTowerType = TowerType.Archer;
        builderTower.StartBuildAnim();
    }

    public void ChoseTesla()
    {
        chosenTowerType = TowerType.Tesla;
        builderTower.StartBuildAnim();
    }

    public void ChoseInfernal()
    {
        chosenTowerType = TowerType.Infernal;
        builderTower.StartBuildAnim();
    }
    
}
