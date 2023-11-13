using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] int startMoney;
    [SerializeField] TextMeshProUGUI moneyText;

    int currentMoney;

    void Awake() 
    {
        currentMoney = startMoney;
        ShowCurrentMoney();  
    }

    public void IncreaseMoney(int amount)
    {
        currentMoney += amount;
        ShowCurrentMoney();
    }

    public void DecreaseMoney(int amount)
    {
        if(currentMoney < amount) { return; }
        currentMoney -= amount;
        ShowCurrentMoney();
    }

    void ShowCurrentMoney()
    {
        moneyText.text = currentMoney.ToString();
    }

    public int GetCurrentMoney
    {
        get
        {
            return currentMoney;
        }
    }

}
