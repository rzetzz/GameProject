using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem hit;
    Transform player;
    Vector3 hitDir;
    public bool isKnockBack;
    [SerializeField] float knockBackPower = 5;
    [SerializeField] float knockBackTime = 0.5f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        hitDir = player.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnockBack)
        {
            StartCoroutine(KnockBack());
        }
        hit.transform.localScale = hitDir;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Attack")
        {
            hitDir = player.transform.localScale;
            Debug.Log("Hit");
            hit.Play();
            isKnockBack = true;
        }
        
    }

    private IEnumerator KnockBack()
    {   rb.velocity = new Vector2(knockBackPower*hitDir.x,rb.velocity.y);
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockBack = false;
        
    }
}
