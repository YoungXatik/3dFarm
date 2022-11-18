using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundController : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;

    [SerializeField] private GameObject groundObjectPrefab;

    [SerializeField] private GameObject leftSide,rightSide,downSide,topSide;
    
    [SerializeField] private int sizeX, sizeY;
    [SerializeField] private float delay;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        SpawnGrounds();
        GenerateSideGround();
        
    }
    
    
    private void SpawnGrounds()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                var clone = Instantiate(groundObjectPrefab,gameObject.transform);
                clone.transform.position = new Vector3(i,0,j);
            }
        }  
        _navMeshSurface.BuildNavMesh();
    }

    private void GenerateSideGround()
    {
        for (int i = 0; i < sizeY; i++)
        {
            var cloneLeftPart = Instantiate(leftSide,gameObject.transform);
            cloneLeftPart.transform.position = new Vector3(-1f,0,i);
            var cloneRightPart = Instantiate(rightSide, gameObject.transform);
            cloneRightPart.transform.position = new Vector3(sizeX,0,i);
        }

        for (int i = 0; i < sizeX + 20; i++)
        {
            var cloneTopSide = Instantiate(topSide, gameObject.transform);
            cloneTopSide.transform.position = new Vector3(-10 + i,0,sizeY - 1); 
            var cloneDownSide = Instantiate(downSide, gameObject.transform);
            cloneDownSide.transform.position = new Vector3(-10 + i,0,0);
        }
    }
}
