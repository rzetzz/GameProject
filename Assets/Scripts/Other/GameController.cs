using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    PlayerHealthController health;
    Rigidbody2D playerRb;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<PlayerHealthController>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Respawn")
        {
            respawnPoint = other.transform;
        }
        if(other.tag =="Traps")
        {
            health.DealDamage();
            StartCoroutine(Respawn(0.3f));
        }
    }
    
    private IEnumerator Respawn (float time)
    {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        if(respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }
        playerRb.simulated = true;
    }


}
