using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDamaged : MonoBehaviour
{

    PlayerHealthController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PlayerHitbox"){

        
            player.DealDamage();
            
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
            if (other.tag == "PlayerHitbox"){

        
            player.DealDamage();
            
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "PlayerHitbox"){
            player.DealDamage();
        }
    }
}
