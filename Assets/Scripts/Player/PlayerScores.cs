using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    [Header("Values")]
    public float countOfCarrots;
    public float experience;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI countOfCarrotsText;
    [SerializeField] private TextMeshProUGUI experienceText;
    
    public void AddExperience(float value)
    {
        DOTween.To(x => experience = x, experience, experience + value, 1f)
            .OnUpdate(() => experienceText.text = $"Experience : {Mathf.RoundToInt(experience)}").SetEase(Ease.Linear);
    }

    public void AddCarrots(float value)
    {
        countOfCarrots += value;
        countOfCarrotsText.text = $"Count of carrots : {countOfCarrots}";
    }
}
