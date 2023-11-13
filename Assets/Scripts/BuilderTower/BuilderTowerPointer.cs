using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuilderTowerPointer : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] AudioSource audioSource;
    [Header("UI Sprite")]
    [SerializeField] Canvas builderCanvas;
    [SerializeField] Canvas archerCanvas;
    [SerializeField] Canvas teslaCanvas;
    [SerializeField] Canvas infernoCanvas;
    [SerializeField] TextMeshProUGUI archerCostText;
    [SerializeField] TextMeshProUGUI teslaCostText;
    [SerializeField] TextMeshProUGUI infernoCostText;

    [Header("CostOfTowers")]
    [SerializeField] int archerCost;
    [SerializeField] int teslaCost;
    [SerializeField] int infernoCost;

    [Header("Components")]
    [SerializeField] BuilderTowerManager builderTowerManager;

    Canvas currentTowerUICanvas;

    void Awake() 
    {
        archerCostText.text = archerCost.ToString();
        teslaCostText.text = teslaCost.ToString();
        infernoCostText.text = infernoCost.ToString();
        currentTowerUICanvas = builderCanvas;
        currentTowerUICanvas.gameObject.SetActive(false);
        archerCanvas.gameObject.SetActive(false);
        teslaCanvas.gameObject.SetActive(false);
        infernoCanvas.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        currentTowerUICanvas.gameObject.SetActive(!currentTowerUICanvas.gameObject.activeSelf);
    }

    #region BUTTON_EVENTS
    public void ChoseArcherTowerButton()
    {
        if(FindObjectOfType<Bank>().GetCurrentMoney < archerCost) { return; }
        PlayHummerHit();
        FindObjectOfType<Bank>().DecreaseMoney(archerCost);
        builderTowerManager.ChoseArcher();
        currentTowerUICanvas.gameObject.SetActive(false);
        currentTowerUICanvas = archerCanvas;
    }

    public void ChoseTeslaTowerButton()
    {
        if(FindObjectOfType<Bank>().GetCurrentMoney < teslaCost) { return; }
        PlayHummerHit();
        FindObjectOfType<Bank>().DecreaseMoney(teslaCost);
        builderTowerManager.ChoseTesla();
        currentTowerUICanvas.gameObject.SetActive(false);
        currentTowerUICanvas = teslaCanvas;
    }

    public void ChoseInfernoTowerButton()
    {
        if(FindObjectOfType<Bank>().GetCurrentMoney < infernoCost) { return; }
        PlayHummerHit();
        FindObjectOfType<Bank>().DecreaseMoney(infernoCost);
        builderTowerManager.ChoseInfernal();
        currentTowerUICanvas.gameObject.SetActive(false);
        currentTowerUICanvas = infernoCanvas;
    }    
    #endregion

    public void ChosedSomething()
    {
        currentTowerUICanvas.gameObject.SetActive(false);
    }

    public void PlayHummerHit()
    {
        audioSource.Play();
    }

    public void DisableSFX()
    {
        audioSource.enabled = false;
    }

}
