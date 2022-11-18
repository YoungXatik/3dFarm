using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsDataBase : MonoBehaviour
{
    public static PlantsDataBase Instance;

    private void Awake()
    {
        Instance = this;
    }

    public PlantData carrot;
    public PlantData tree;
    public PlantData grass;
}
