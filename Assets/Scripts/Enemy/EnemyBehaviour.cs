using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int health = 14;
    [SerializeField] private ParticleSystem hit;
    [SerializeField] private ParticleSystem death;
    Transform player;
    Vector3 hitDir;
    public bool isKnockBack;
    [SerializeField] float knockBackPower = 5;
    [SerializeField] float knockBackTime = 0.5f;
    Rigidbody2D rb;
    [SerializeField]Collider2D attack;
    [SerializeField]Material matBlink;
    Material defaultMat;
    SpriteRenderer sr;
    Collider2D body;
    Animator setAnim;
    PlayerController playerControl;
    bool hasPlayed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        body = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        hitDir = player.transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        setAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // if (isKnockBack)
        // {
        //     StartCoroutine(KnockBack());
        // }
        // hit.transform.localScale = hitDir;
        if(health <= 0)
        {   
            
            setAnim.SetTrigger("isDead");
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Attack")
        {
            if(playerControl.isFinalAttack)
            {
                health -= PlayerStats.instance.attackFinalDamage;
            }
            else
            {
                health -= PlayerStats.instance.attackDamage;
            }
            hitDir = player.transform.localScale;    
            hit.transform.rotation = getPosition();
            StartCoroutine(Blink());
            StartCoroutine(KnockBack());
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

    IEnumerator Blink()
    {
        sr.material = matBlink;
        yield return new WaitForSeconds(0.1f);
        sr.material = defaultMat;
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
        
    }
    void stopAttack()
    {
        attack.enabled = false;
    }
    void setDead()
    {
        if(!hasPlayed)
        {
            death.Play();
            hasPlayed = true;
        }
        
        sr.enabled = false;
        body.enabled = false;
        rb.velocity = new Vector2(0f,rb.velocity.y);
        // rb.gravityScale = 0;
    }
    void setDisabled()
    {
        this.gameObject.SetActive(false);
    }
    void setBody()
    {
        body.enabled = true;
    }
}
