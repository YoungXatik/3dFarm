using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera mainCamera;
    public float timeToChangeCameraPosition;
    
    [SerializeField] private Vector3 startCameraPosition;
    
    [SerializeField] private Vector3 currentPlantCameraPosition;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        startCameraPosition = mainCamera.transform.position;
    }

    public void ReturnCameraPositionToDefault()
    {
        mainCamera.transform.DOMove(startCameraPosition, timeToChangeCameraPosition).SetEase(Ease.Linear);
    }

    public void ChangeCameraPositionToPlant(Vector3 plantPosition)
    {
        currentPlantCameraPosition = plantPosition + offset;
        mainCamera.transform.DOMove(currentPlantCameraPosition, timeToChangeCameraPosition).SetEase(Ease.Linear);
    }
}
