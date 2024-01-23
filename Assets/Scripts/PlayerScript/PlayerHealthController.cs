using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{

    
    bool isAttack;
    private static bool hasRun = true;
    [SerializeField]float immuneLength = 0.15f;
    [SerializeField]float blinkTime = 0.15f;
    float immuneCounter;
    float blinkCounter;
    SpriteRenderer sr;
    PlayerController control;
    [SerializeField] DataSave data;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        if(hasRun)
        {
            data.sceneIndex = 0;
            data.currentHealth = data.maxHealth;
            hasRun = false;
        }
        
        // PlayerStats.instance.currentHealth = PlayerStats.instance.maxHealth;
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

        if(data.currentHealth <= 0)
        {
            data.playerDead = true;
            StartCoroutine(ReloadLevel());
        }
    }
    public void DealHalfDamage()
    {
        PlayerStats.instance.currentHealth--;
        if(PlayerStats.instance.currentHealth < 0)
        {
            
        }
    }
    public void DealDamage()
    {
        if(immuneCounter<=0)
        {
    
            data.currentHealth -= 2;
            StartCoroutine(control.KnockBack());
            immuneCounter = immuneLength;
        }
        
    }
    IEnumerator Blinking()
    {
         sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .5f);
         yield return new WaitForSeconds(0.5f);
         sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
    }

    private void OnApplicationQuit() {
        hasRun = true;
    }

    IEnumerator ReloadLevel()
    {
        Debug.Log("Dead Now");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(data.checkpointSceneIndex,LoadSceneMode.Single);
    }
    

}
