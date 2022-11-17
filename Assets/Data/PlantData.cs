using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant",menuName = "Create New Plant")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public float growTime;
    public float expReward;

    public GameObject plantObject;
}
