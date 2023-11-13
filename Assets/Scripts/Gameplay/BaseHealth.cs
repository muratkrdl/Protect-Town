using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] int baseMaxHitPoints;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] CanvasController canvasController;

    int currentHitPoints;

    void Awake() 
    {
        currentHitPoints = baseMaxHitPoints;
        ShowHealthText();
    }

    public void DecreaseHitPoints(int amount)
    {
        currentHitPoints -= amount;
        if(currentHitPoints <= 0)
        {
            //Game over
            currentHitPoints = 0;
            canvasController.ShowGameOver();
        }
        ShowHealthText();
    }

    void ShowHealthText()
    {
        healthText.text = currentHitPoints.ToString();
    }

}
