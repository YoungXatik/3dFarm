using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaceComponent : MonoBehaviour, IPointerClickHandler
{
    private Outline _outline;
    [SerializeField] private NavMeshObstacle _navMeshObstacle;

    public bool isPlanted;
    public bool isPlantCompletlyGrowed;
    public bool canClick = true;
    public PlantData currentPlacePlant;

    public GameObject currentPlacedPlantObject;

    [Header("UI")] 
    [SerializeField] private GameObject fillImageBg;
    [SerializeField] private Image fillImage;
    
    private void Start()
    {
        _outline = GetComponentInChildren<Outline>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        fillImageBg.SetActive(false);
    }

    public void ThisPlaceClicked()
    {
        if (PlayerTools.Instance.playerMovement.canMove)
        {
            if (canClick)
            {
                PlayerTools.Instance.currentPickedPlace = this;
                PlayerTools.Instance.ShowPlayerTools();
                _outline.enabled = true;
                PlayerTools.Instance.playerMovement.currentClickedPoint = transform.position;
                PlayerTools.Instance.playerMovement.MovePlayerToPoint(transform.position);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void ThisPlaceUnClicked()
    {
        _outline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerTools.Instance.currentPickedPlace == null)
        {
            ThisPlaceClicked();
        }
        else
        {
            PlayerTools.Instance.currentPickedPlace.ThisPlaceUnClicked();
            ThisPlaceClicked();
        }
    }

    public void StartGrowing(GameObject plant)
    {
        if (currentPlacePlant != null)
        {
            plant.transform.DOScale(1, currentPlacePlant.growTime).From(0);
            fillImageBg.SetActive(true);
            fillImageBg.transform.DOScale(1, 1).From(0);
            DOTween.To(x => fillImage.fillAmount = x, 0, 1, currentPlacePlant.growTime).OnComplete(OnPlantFinishGrowing);
            canClick = false;
        }
    }

    private void OnPlantFinishGrowing()
    {
        isPlantCompletlyGrowed = true;
        if (PlayerTools.Instance.currentPickedPlace == this)
        {
            if (currentPlacePlant == PlantsDataBase.Instance.tree)
            {
                return;
            }
            else
            {
                PlayerTools.Instance.ShowPlayerTools();   
            }
        }

        if (currentPlacePlant == PlantsDataBase.Instance.tree)
        {
            canClick = false;
            _navMeshObstacle.enabled = true;
        }
        else
        {
            canClick = true;
        }
        Debug.Log("Finished!");
    }

    public void OnPlantTake()
    {
        currentPlacePlant = null;
        isPlanted = false;
        isPlantCompletlyGrowed = false;
        fillImageBg.SetActive(false);
        Destroy(currentPlacedPlantObject);
        currentPlacedPlantObject = null;
    }
}
