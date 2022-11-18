using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    public Shop shop;
    [Header("Values")]
    public float countOfCarrots;
    public float experience;
    public float money;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI countOfCarrotsText;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Animator scoresAnimator;
    
    public void AddExperience(float value)
    {
        scoresAnimator.SetBool("Start",true);
        DOTween.To(x => experience = x, experience, experience + value, 1f)
            .OnUpdate(() => experienceText.text = $"Experience : {Mathf.RoundToInt(experience)}").SetEase(Ease.Linear);
    }

    public void AddCarrots(float value)
    {
        scoresAnimator.SetBool("Start",true);
        countOfCarrots += value;
        countOfCarrotsText.text = $"Count of carrots : {countOfCarrots}";
        shop.UpdateCarrotsCount();
    }

    public void CloseScoreTab()
    {
        scoresAnimator.SetBool("Start",false);
    }

    public void RemoveCarrot(int value)
    {
        countOfCarrots -= 1;
        DOTween.To(x => money = x, money, money + value, 1f).
            OnUpdate(() => moneyText.text = $"Money : {Mathf.RoundToInt(money)}").SetEase(Ease.Linear);
    }
}
