using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DataSave", menuName = "ScriptableObjects/PlayerHealthData", order = 1)]
public class DataSave : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public int sceneIndex;
}

