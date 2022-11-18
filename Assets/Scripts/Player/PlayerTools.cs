using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public static PlayerTools Instance;
    private PlayerScores _playerScores;
    [SerializeField] private CameraMovement _cameraMovement;

    private void Awake()
    {
        Instance = this;
        _playerScores = GetComponent<PlayerScores>();
    }

    [SerializeField] private GameObject playerToolsMenu;
    [SerializeField] private GameObject playerToolsTakePlantButton;

    public PlaceComponent currentPickedPlace;

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
                currentPickedPlace.currentPlacePlant = data;
                var plant = Instantiate(data.plantObject, currentPickedPlace.transform.position, Quaternion.identity,currentPickedPlace.transform);
                currentPickedPlace.isPlanted = true;
                currentPickedPlace.StartGrowing(plant);
                currentPickedPlace.currentPlacedPlantObject = plant;
            }
        }
    }

    public void TakePlant()
    {
        StartCoroutine(TakePlantCoroutine());
    }

    private IEnumerator TakePlantCoroutine()
    {
        if (currentPickedPlace.isPlantCompletlyGrowed)
        {
            if (currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.carrot)
            {
                _playerScores.AddCarrots(1);
                _playerScores.AddExperience(currentPickedPlace.currentPlacePlant.expReward);
            }
            else if(currentPickedPlace.currentPlacePlant == PlantsDataBase.Instance.grass)
            {
                _playerScores.AddExperience(currentPickedPlace.currentPlacePlant.expReward);
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
