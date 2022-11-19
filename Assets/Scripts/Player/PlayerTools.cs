using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public static PlayerTools Instance;
    [SerializeField] private PlayerScores _playerScores;
    [SerializeField] private CameraMovement _cameraMovement;
    public PlayerMovement playerMovement;
    [SerializeField] private ItemsAnimationEvents _itemsAnimationEvents;

    private void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>();
    }

    [Header("UI")]
    [SerializeField] private GameObject playerToolsMenu;
    [SerializeField] private GameObject playerToolsTakePlantButton;

    [Header("Values")]
    public PlaceComponent currentPickedPlace;
    [SerializeField] private AnimationClip plantAnimation;
    private float animationDuration;

    [Header("Particles")]
    [SerializeField] private GameObject plantParticle;
    [SerializeField] private GameObject takePlantParticle;
    [SerializeField] private GameObject experiencePopup, carrotPopup;

    private void Start()
    {
        HidePlayerTools();
    }
    public void ShowPlayerTools()
    {
        playerToolsMenu.SetActive(true);
        if (currentPickedPlace.isPlantCompletlyGrowed)
        {
            if (currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.tree)
            {
                playerToolsTakePlantButton.SetActive(false);
            }
            else
            {
                playerToolsTakePlantButton.SetActive(true);
            }
        }
        else
        {
            playerToolsTakePlantButton.SetActive(false);   
        }
    }

    public void HidePlayerTools()
    {
        playerToolsMenu.SetActive(false);
        playerToolsTakePlantButton.SetActive(false);
        if (currentPickedPlace != null)
        {
            currentPickedPlace.ThisPlaceUnClicked();
            currentPickedPlace = null;
        }
    }

    public void DoPlant(PlantData data)
    {
        currentPickedPlace.currentPlacePlant = data;
        if (currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.carrot)
        {
            _itemsAnimationEvents.currentPlantingObject = _itemsAnimationEvents.plantCarrot;
        }
        else if (currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.grass)
        {
            _itemsAnimationEvents.currentPlantingObject = _itemsAnimationEvents.plantGrass;
        }
        else if (currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.tree)
        {
            _itemsAnimationEvents.currentPlantingObject = _itemsAnimationEvents.plantTree;
        }
        playerMovement.playerAnimator.SetBool("Plant",true);
    }

    public void Planting()
    {
        if (currentPickedPlace != null)
        {
            if (!currentPickedPlace.isPlanted)
            {
                var plant = Instantiate(currentPickedPlace.currentPlacePlant.plantObject, currentPickedPlace.transform.position, Quaternion.identity,currentPickedPlace.transform);
                currentPickedPlace.isPlanted = true;
                currentPickedPlace.StartGrowing(plant);
                currentPickedPlace.currentPlacedPlantObject = plant;
                GrowParticle();
                HidePlayerTools();
            }
        }
    }

    public void TakePlant()
    {
        StartCoroutine(TakePlantCoroutine());
        playerToolsMenu.SetActive(false);
        playerToolsTakePlantButton.SetActive(false);
        TakePlantAnimation();
    }

    private void TakePlantAnimation()
    {
        Instantiate(takePlantParticle, new Vector3(transform.position.x,1.5f,transform.position.z), Quaternion.identity);
        var popupExp = Instantiate(experiencePopup);
        Destroy(popupExp,1f);
        var popupCarrot = Instantiate(carrotPopup);
        Destroy(popupCarrot,1);
    }

    private void GrowParticle()
    {
        Instantiate(plantParticle, new Vector3(transform.position.x,1.5f,transform.position.z), Quaternion.identity);
    }

    private IEnumerator TakePlantCoroutine()
    {
        if (currentPickedPlace.isPlantCompletlyGrowed)
        {
            playerMovement.playerAnimator.SetBool("Take",true);
            if (currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.carrot)
            {
                _playerScores.AddCarrots(1);
                _playerScores.AddExperience(currentPickedPlace.currentPlacePlant.expReward);
                _itemsAnimationEvents.currentTakingObject = _itemsAnimationEvents.takeCarrot;
            }
            else if(currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.grass)
            {
                _playerScores.AddExperience(currentPickedPlace.currentPlacePlant.expReward);
                _itemsAnimationEvents.currentTakingObject = _itemsAnimationEvents.takeGrass;
            }
            _cameraMovement.ChangeCameraPositionToPlant(currentPickedPlace.transform.position);
            yield return new WaitForSeconds(_cameraMovement.timeToChangeCameraPosition + 0.1f);
            currentPickedPlace.OnPlantTake();
            yield return new WaitForSeconds(_cameraMovement.timeToChangeCameraPosition * 2);
            _cameraMovement.ReturnCameraPositionToDefault();
            HidePlayerTools();
            yield return new WaitForSeconds(_cameraMovement.timeToChangeCameraPosition);
            _cameraMovement.GiveCameraPlayerObjectToFollow();
        }
    }
}
