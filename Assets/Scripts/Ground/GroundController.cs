using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundController : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;

    [SerializeField] private GameObject groundObjectPrefab;
    
    [SerializeField] private int sizeX, sizeY;
    [SerializeField] private float delay;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        StartCoroutine(SpawnGrounds());
    }
    
    
    private IEnumerator SpawnGrounds()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                var clone = Instantiate(groundObjectPrefab,gameObject.transform);
                clone.transform.position = new Vector3(i,0,j);
                yield return new WaitForSeconds(delay);
            }
        }  
        _navMeshSurface.BuildNavMesh();
    }
}
