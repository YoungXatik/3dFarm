using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAnimationEvents : MonoBehaviour
{
    [Header("TakePlantsObjects")]
    public GameObject takeCarrot;
    public GameObject takeGrass;
    [Header("PlantObjects")]
    public GameObject plantCarrot;
    public GameObject plantGrass;
    public GameObject plantTree;

    public GameObject currentTakingObject;
    public GameObject currentPlantingObject;

    private Animator playerAnimator;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void AllowPlayerMove()
    {
        _playerMovement.canMove = true;
    }

    public void BanPlayerMove()
    {
        _playerMovement.canMove = false;
    }

    public void ShowTakingObject()
    {
        currentTakingObject.SetActive(true);
    }

    public void HideTakingObject()
    {
        currentTakingObject.SetActive(false);
    }
    
    public void ShowPlantingObject()
    {
        currentPlantingObject.SetActive(true);
    }

    public void HidePlantingObject()
    {
        currentPlantingObject.SetActive(false);
    }

    public void CancelTakingPlants()
    {
        playerAnimator.SetBool("Take",false);
    }

    public void CancelPlanting()
    {
        playerAnimator.SetBool("Plant",false);
    }
}
