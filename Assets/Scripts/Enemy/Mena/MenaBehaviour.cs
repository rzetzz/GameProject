using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenaBehaviour : MonoBehaviour
{
    [SerializeField] Transform maxBounds, minBounds;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float attackSpeed = 10;
    [SerializeField] float aggroDistance,aggroAttackDistance;
    [SerializeField] float ChaseTime = 5;
    [SerializeField] float waitTime = 3;
    [SerializeField] float bulletTime = 3;
    [SerializeField] float bulletSpeed = 3;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletLocation;
    float waitCounter;
    float chaseCounter;
    private Vector3 loc;
    private Vector3 moveDir;
    private Vector3 attackDir;
    private Rigidbody2D rb;
    private Transform player;
    private Animator setAnim;
    private EnemyBehaviour enemy;
    bool hasPosition;
    bool hasAttackDir;
    bool isFacingLeft;
    bool isChase;
    bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        setAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyBehaviour>();
        maxBounds.SetParent(null);
        minBounds.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
        // if (rb.velocity.x > 0 && !isFacingLeft)
        // {
        //     Flip();
        // } else if(rb.velocity.x < 0 && isFacingLeft)
        // {
        //     Flip();
        // }
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
        }
        if (GetPlayerPosition())
        {
            chaseCounter = ChaseTime;
            if(waitCounter <= 0)
            {
                isChase = true;
            }
        }

        if(CanAttack())
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
        if(chaseCounter>0)
        {
            
            chaseCounter-=Time.deltaTime;
        }
        else
        {
            isChase = false;
        }
        setAnim.SetBool("isChase",isChase);
        setAnim.SetBool("isAttack",isAttack);
    }
    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 kukuScale = transform.localScale;
        kukuScale.x *= -1;
        transform.localScale = kukuScale;
    }
    void FlipTowardsPlayer(){
        float playerPosition = player.position.x - transform.position.x;
        
        if (playerPosition > 0 && !isFacingLeft){
            Flip();
        } else if (playerPosition < 0 && isFacingLeft){
            Flip();
        }
    }

    public void Flying()
    {
        if(!hasPosition)
        {
            loc = new Vector3(Random.Range(minBounds.position.x,maxBounds.position.x), Random.Range(minBounds.position.y,maxBounds.position.y),transform.position.z);
            hasPosition = true;
        }
        moveDir = loc - transform.position;
        moveDir.Normalize();

        if(Vector3.Distance(transform.position, loc) > .2f)
        {
            if(!enemy.isKnockBack)
            {
                rb.velocity = moveDir * moveSpeed;
            }
            
            if(transform.position.x > loc.x){
                if(isFacingLeft)
                {
                    Flip();
                }                
            } else if(transform.position.x < loc.x) {
                if(!isFacingLeft)
                {
                    Flip();
                }
                                             
            }
        }
        else
        {
            hasPosition = false;
        }   

        
    }
    public void RandomState()
    {
        int rand = Random.Range(1,3);
        // rand = 2;
        switch (rand)
        {
            case 1 :
                setAnim.SetTrigger("AttackChase");
                break;
            case 2 :
                setAnim.SetTrigger("AttackShoot");
                break;
            
        }
    }

    void ShotBullet()
    {
        if(!hasAttackDir)
        {
            attackDir = PlayerDir();
            hasAttackDir = true;
        }
        bullet.SetActive(true);
        bullet.transform.SetParent(null);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = attackDir * bulletSpeed;
        StartCoroutine(bulletReturn());
    }

    public void Chasing()
    {
        if(!enemy.isKnockBack)
        {
            rb.velocity = PlayerDir() * (moveSpeed+1);
        }
        
    }
    public void Attacking()
    {
        if(!hasAttackDir)
        {
            attackDir = PlayerDir();
            hasAttackDir = true;
        }
        if(!enemy.isKnockBack)
        {
            rb.velocity = attackDir * attackSpeed;
        }
    }

    Vector3 PlayerDir()
    {
        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        return dir;
    }
    float Distance()
    {
        return Vector3.Distance(transform.position,player.position);
    }
    bool GetPlayerPosition()
    {
        
        if(Distance() < aggroDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CanAttack()
    {
        if(Distance()< aggroAttackDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator bulletReturn()
    {
        yield return new WaitForSeconds(bulletTime);
        bullet.transform.SetParent(this.transform);
        bullet.transform.position = bulletLocation.position;
        bullet.SetActive(false);
    }
    void resetAttackPos()
    {
        hasAttackDir = false;
    }
    void StopMove()
    {
        rb.velocity = Vector2.zero;
    }
    void setWait()
    {
        isChase = false;
        waitCounter = waitTime;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
        Gizmos.DrawWireSphere(transform.position, aggroAttackDistance);
    }
}
