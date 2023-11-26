using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{

    bool isAttack;
    [SerializeField]float immuneLength = 0.15f;
    [SerializeField]float blinkTime = 0.15f;
    float immuneCounter;
    float blinkCounter;
    SpriteRenderer sr;
    PlayerController control;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
        control = GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(immuneCounter>0)
        {
            immuneCounter-=Time.deltaTime;
             if(blinkCounter >0){
                blinkCounter -= Time.deltaTime;
            }else if (blinkCounter<=0){
                blinkCounter = blinkTime;
            }
            if (blinkCounter > 0.1){
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
            } else if (blinkCounter < 0.1 ){
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
                
            } 
        }
        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }
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
        if(immuneCounter<=0)
        {
    
            PlayerStats.instance.currentHealth -= 2;
            StartCoroutine(control.KnockBack());
            immuneCounter = immuneLength;
        }
        if(PlayerStats.instance.currentHealth < 0)
        {
            Debug.Log("Dead Now");
        }
    }
    IEnumerator Blinking()
    {
         sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
         yield return new WaitForSeconds(0.5f);
         sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    

}
