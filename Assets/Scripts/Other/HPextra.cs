using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPextra : MonoBehaviour
{
    [SerializeField] DataSave data;
    [SerializeField] int HpId;
    void Start()
    {
        if (Display(HpId))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Taken(HpId);
            data.maxHealth += 2;
            data.currentHealth = data.maxHealth;
            gameObject.SetActive(false);
        }
    }

    void Taken(int id)
    {
        switch (id)
        {
            case 1:
                data.hp1 = true;
                break;
            case 2:
                data.hp2 = true;
                break;
            case 3:
                data.hp3 = true;
                break;  
            case 4:
                data.hp4 = true;
                break;  
            case 5:
                data.hp5 = true;
                break;  
            case 6:
                data.hp6 = true;
                break;  
            case 7:
                data.hp7 = true;
                break;  
                      
        }
    }

    bool Display(int id)
    {
        switch (id)
        {
            case 1:
                return data.hp1;
            case 2:
                return data.hp2;
            case 3:
                return data.hp3;  
            case 4:
                return data.hp4; 
            case 5:
                return data.hp5; 
            case 6:
                return data.hp6; 
            case 7:
                return data.hp7; 

            default:
                return data.hp1;    
        }
    }

}
