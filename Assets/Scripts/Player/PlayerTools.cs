using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public static PlayerTools Instance;
    private PlayerScores _playerScores;
    [SerializeField] private CameraMovement _cameraMovement;
    public PlayerMovement playerMovement;
    [SerializeField] private ItemsAnimationEvents _itemsAnimationEvents;

    private void Awake()
    {
        Instance = this;
        _playerScores = GetComponent<PlayerScores>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    [SerializeField] private GameObject playerToolsMenu;
    [SerializeField] private GameObject playerToolsTakePlantButton;

    public PlaceComponent currentPickedPlace;

    [Header("Particles")]
    [SerializeField] private GameObject plantParticle;
    [SerializeField] private GameObject takePlantParticle;

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
        if (currentPickedPlace != null)
        {
            if (!currentPickedPlace.isPlanted)
            {
                playerMovement.playerAnimator.SetBool("Plant",true);
                currentPickedPlace.currentPlacePlant = data;
                var plant = Instantiate(data.plantObject, currentPickedPlace.transform.position, Quaternion.identity,currentPickedPlace.transform);
                currentPickedPlace.isPlanted = true;
                currentPickedPlace.StartGrowing(plant);
                currentPickedPlace.currentPlacedPlantObject = plant;
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
                Instantiate(plantParticle, new Vector3(transform.position.x,1.5f,transform.position.z), Quaternion.identity);
                HidePlayerTools();
            }
        }
    }

    public void TakePlant()
    {
        StartCoroutine(TakePlantCoroutine());
        playerToolsMenu.SetActive(false);
        playerToolsTakePlantButton.SetActive(false);
        Instantiate(takePlantParticle, new Vector3(transform.position.x,1.5f,transform.position.z), Quaternion.identity);
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
            yield return new WaitForSeconds(0.5f);
            _cameraMovement.ReturnCameraPositionToDefault();
            HidePlayerTools();
        }
    }
}
