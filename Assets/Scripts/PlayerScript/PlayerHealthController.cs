using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DealHalfDamage()
    {
        PlayerStats.instance.currentHealth--;
        if(PlayerStats.instance.currentHealth < 0)
        {
            Debug.Log("Dead Now");
        }
    }
    public void DealDamage()
    {
        PlayerStats.instance.currentHealth -= 2;
        if(PlayerStats.instance.currentHealth < 0)
        {
            Debug.Log("Dead Now");
        }
    }

}
