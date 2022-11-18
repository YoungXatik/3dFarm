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
    }

    public void CloseScoreTab()
    {
        scoresAnimator.SetBool("Start",false);
    }
}
