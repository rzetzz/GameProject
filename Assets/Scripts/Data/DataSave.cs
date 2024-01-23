using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DataSave", menuName = "ScriptableObjects/PlayerHealthData", order = 1)]
public class DataSave : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public int sceneIndex;
    public int checkpointSceneIndex,checkpointId;
    public bool playerFaceRight;
    public bool canDoubleJump,canDash,canWallJump,canChargeDash,canAttackBow;
    public bool event1;
    public bool playerDead;
    public bool hp1,hp2,hp3,hp4,hp5,hp6,hp7;
}

