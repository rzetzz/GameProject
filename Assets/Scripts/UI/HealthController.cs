using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image[] health;
    public Sprite fullHealth;
    public Sprite emptyHealth;
    public Sprite halfHealth;
    public Sprite primaryFull;
    public Sprite primaryHalf;

    [SerializeField] DataSave data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         for (int i = 0; i < health.Length; i++)
        {
            if ( i < data.maxHealth/2){
                health[i].enabled = true;
            } else {
                health[i].enabled = false;
            }
            

            if(i != 0)
            {
                if (i < data.currentHealth/2)
                {
                    health[i].sprite = fullHealth; 
                } 
                else if (i > (data.currentHealth/2)-1 && data.currentHealth % 2 != 0 && i < (data.currentHealth/2)+1 )
                {
                    health[i].sprite = halfHealth;         
                } 
                else if (i > (data.currentHealth/2)-2 && data.currentHealth % 2 == 0 || data.currentHealth == 0)
                {
                    health[i].sprite = emptyHealth;
                    
                }
            }
            if(i == 0)
            {
                if (i < data.currentHealth/2)
                {
                    health[i].sprite = primaryFull; 
                } 
                else if (i > (data.currentHealth/2)-1 && data.currentHealth % 2 != 0 && i < (data.currentHealth/2)+1 )
                {
                    health[i].sprite = primaryHalf;         
                } 
                else if (i > (data.currentHealth/2)-2 && data.currentHealth % 2 == 0 || data.currentHealth == 0)
                {
                    health[i].sprite = emptyHealth;
                    
                }
            }




        }
    }
}
