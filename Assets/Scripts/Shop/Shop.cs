using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerScores playerScores;
    [Header("Values")]
    public int carrotCost;
    public float countOfCarrots;
    public int upgradeCost;
    [SerializeField] private Animator shopAnimator;
    public bool isOpen;

    [Header("UI")]
    [SerializeField] private Button openShopButton;
    
    [SerializeField] private Button sellCarrotButton;
    [SerializeField] private Button buyUpgradeButton;
    
    [SerializeField] private TextMeshProUGUI buyUpgradeText;
    [SerializeField] private TextMeshProUGUI countOfCarrotsText;

    private void Start()
    {
        countOfCarrots = playerScores.countOfCarrots;
        buyUpgradeText.text = $"BUY ({upgradeCost}$)";
        countOfCarrotsText.text = $"x{countOfCarrots}";
        UpdateCarrotsCount();
        UpdateMoneyCount();
    }

    public void UpdateMoneyCount()
    {
        if (playerScores.money >= upgradeCost)
        {
            buyUpgradeButton.interactable = true;
        }
        else
        {
            buyUpgradeButton.interactable = false;
        }
    }
    
    public void BuyUpgrade()
    {
        if (playerScores.money >= upgradeCost)
        {
            buyUpgradeButton.interactable = false;
            buyUpgradeText.text = "READY";
            PlantsDataBase.Instance.carrot.growTime /= 2;
            PlantsDataBase.Instance.tree.growTime /= 2;
            PlantsDataBase.Instance.grass.growTime /= 2;
        }
        else
        {
            return;
        }
    }

    public void SellCarrot()
    {
        if (playerScores.countOfCarrots != 0)
        {
            playerScores.RemoveCarrot(carrotCost);
            UpdateCarrotsCount();
            UpdateMoneyCount();
        }
        else
        {
            return;
        }
        
    }

    public void UpdateCarrotsCount()
    {
        countOfCarrots = playerScores.countOfCarrots;
        countOfCarrotsText.text = $"x{countOfCarrots}";
        if (countOfCarrots <= 0)
        {
            sellCarrotButton.interactable = false;
        }
        else
        {
            sellCarrotButton.interactable = true;   
        }
    }

    public void OpenShop()
    {
        if (isOpen)
        {
            shopAnimator.SetBool("Open",false);
            isOpen = false;
        }
        else
        {
            shopAnimator.SetBool("Open",true);
            shopAnimator.SetBool("idle",false);
            isOpen = true;
        }
    }

    public void IdleState()
    {
        shopAnimator.SetBool("idle",true);
    }
}
