using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public static PlayerTools Instance;

    private void Awake()
    {
        Instance = this;
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
            playerToolsTakePlantButton.SetActive(true);
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
        if (currentPickedPlace.isPlantCompletlyGrowed)
        {
            currentPickedPlace.OnPlantTake();
            HidePlayerTools();
        }
    }
}
