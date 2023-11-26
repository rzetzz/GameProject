using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int currentHealth;
    public int maxHealth = 22;
    public int attackDamage = 3;
    public int attackFinalDamage = 5;
    // Start is called before the first frame update
    private void Awake() {
        instance = this;
    }
   
}
