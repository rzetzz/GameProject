using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    [SerializeField] int checkId;
    [SerializeField] GameObject sign;
    [SerializeField] DataSave data;
    bool canRest;

    PlayerController control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canRest)
        {
            if(control.isRest)
            {
                control.transform.position = new Vector3(transform.position.x,control.transform.position.y,0f);
                sign.SetActive(false);
            }
            else
            {
                sign.SetActive(true);
            }
            
            if (PlayerInputSetting.instance.interact && !control.isRest)
            {
                
                data.currentHealth = data.maxHealth;
                control.isRest = true;
                data.checkpointId = checkId;
                data.checkpointSceneIndex = SceneManager.GetActiveScene().buildIndex;
                Debug.Log("rest");
            } else if(PlayerInputSetting.instance.interact && control.isRest)
            {
                control.isRest = false;
            }
        }
        else
        {
            sign.SetActive(false);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canRest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            canRest = false;
        }
    }

    
}
