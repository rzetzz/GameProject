using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem hit;
    
    [SerializeField] private ParticleSystem hitDown;
    
    [SerializeField] private ParticleSystem hitUp;
    Transform player;
    Vector3 hitDir;
    public bool isKnockBack;
    [SerializeField] float knockBackPower = 5;
    [SerializeField] float knockBackTime = 0.5f;
    Rigidbody2D rb;
    [SerializeField]Collider2D attack;
    Collider2D body;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        body = GetComponent<Collider2D>();
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
        // hit.transform.localScale = hitDir;
        Vector3 dir = player.transform.position - transform.position;
        Debug.Log(dir);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Attack")
        {
            hitDir = player.transform.localScale;
            
            // if(KnockBackDir().y > 0.5f)
            // {
            //     hitDown.Play();
            // }
            // else if (KnockBackDir().y < -0.5f)
            // {
            //     hitUp.Play();
            // }
            // else
            // {
            //     hit.Play();
            // }
            
            hit.transform.rotation = getPosition();
            hit.Play();
            isKnockBack = true;
        }
        
        
    }

    private IEnumerator KnockBack()
    {   
        // rb.velocity = new Vector2(knockBackPower*hitDir.x,rb.velocity.y);
        rb.velocity = -KnockBackDir() * knockBackPower;
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        isKnockBack = false;
        
    }
    Vector3 KnockBackDir(){
        Vector3 dir = player.transform.position - transform.position;
        dir.Normalize();
        return dir;
    }

    
    public Quaternion getPosition(){

        float angle = Mathf.Atan2(-KnockBackDir().y, -KnockBackDir().x) * Mathf.Rad2Deg; 
        
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        
        return targetRot;

       
    }

    void setAttack()
    {
        attack.enabled = true;
        body.enabled = false;
    }
    void stopAttack()
    {
        attack.enabled = false;
    }
    void setBody()
    {
        body.enabled = true;
    }
}
