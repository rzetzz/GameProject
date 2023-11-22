using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamaged : MonoBehaviour
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
        player.DealDamage();
    }
}
