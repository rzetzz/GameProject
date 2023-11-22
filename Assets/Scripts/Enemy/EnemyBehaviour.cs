using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem hit;
    Transform player;
    Vector3 hitDir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hitDir = player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        hit.transform.localScale = hitDir;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Attack")
        {
            hitDir = player.transform.localScale;
            Debug.Log("Hit");
            hit.Play();
        }
        
    }
}
