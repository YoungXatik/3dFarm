using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    public float timeToChangeCameraPosition;
    
    [SerializeField] private Vector3 startCameraPosition;
    
    [SerializeField] private Vector3 currentPlantCameraPosition;
    [SerializeField] private Vector3 currentPlantCameraRotation;
    [SerializeField] private Vector3 offset;

    public Transform playerObject;

    private void Awake()
    {
        _camera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        startCameraPosition = _camera.transform.position;
    }

    public void ReturnCameraPositionToDefault()
    {
        _camera.gameObject.transform.DOMove(startCameraPosition, timeToChangeCameraPosition).SetEase(Ease.Linear);
    }

    public void GiveCameraPlayerObjectToFollow()
    {
        _camera.Follow = playerObject;
    }

    public void ChangeCameraPositionToPlant(Vector3 plantPosition)
    {
        _camera.Follow = null;
        startCameraPosition = _camera.transform.position;
        currentPlantCameraPosition = plantPosition + offset;
        _camera.gameObject.transform.DOMove(currentPlantCameraPosition, timeToChangeCameraPosition).SetEase(Ease.Linear);
    }
}
