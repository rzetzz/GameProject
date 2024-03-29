using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KukuBehaviour : MonoBehaviour
{
    Transform player;
    [SerializeField]private float aggroDistance = 1;
    [SerializeField]private float aggroAttackDistance = 1;
    Rigidbody2D rb;
    [SerializeField]private float attackPower = 2;
    [SerializeField]private float attackTime = 2;
    [SerializeField]private float waitTime = 2;
    [SerializeField]private float ChaseTime = 2;
    [SerializeField]private float jumpHeight = 5;
    [SerializeField] Transform[] locPoint;
    private int currentPoint;
    private float attackCounter;
    private float chaseCounter;
    private float waitCounter;
    private bool isFacingLeft = true;
    private bool isAttack;
    private bool isChase;
    private bool isMove;
    private bool gotPlayer;
    float moveDir;
    [SerializeField] float speed;
    Animator setAnim;
    EnemyBehaviour enemy;
    // Start is called before the first frame update
    void Start()
    {
        setAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = GetComponent<EnemyBehaviour>();
        rb = GetComponent<Rigidbody2D>();
        foreach(Transform poin in locPoint)
        {
            poin.SetParent(null);
        }
        currentPoint = Random.Range(0,2);
        
        
    }

    private void Update() {
        if(chaseCounter>0)
        {
            
            chaseCounter-=Time.deltaTime;
        }
        else
        {
            isChase = false;
        }
        if(GetPlayerPosition())
        {
            chaseCounter = ChaseTime;
            setAnim.SetTrigger("GetPlayer");
            isChase = true;
        }
        if(CanAttack())
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
        setAnim.SetBool("isChasing",isChase);
        setAnim.SetBool("isAttacking",isAttack);
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
    public void Chasing()
    {
        if(!enemy.isKnockBack)
        {
            rb.velocity = new Vector2((speed+1f)*-transform.localScale.x,rb.velocity.y);
        }
    }
    public void Attacking()
    {
        if(!enemy.isKnockBack)
        {
            rb.velocity = new Vector2(attackPower*-transform.localScale.x,rb.velocity.y);
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
                setAnim.SetTrigger("JumpAttack");
                break;
            
        }
    }
    public void JumpToPlayer()
    {
        float playerDir = (player.position.x) - transform.position.x;
        if(!enemy.isKnockBack)
        {
            rb.velocity = new Vector2(playerDir + (-transform.localScale.x), jumpHeight);
        }
    }
    public void MovingPerPoint()
    {
        if (Mathf.Abs(transform.position.x - locPoint[currentPoint].position.x) > .2f){
            if(transform.position.x > locPoint[currentPoint].position.x){
                if(!isFacingLeft)
                {
                    Flip();
                }
                
                moveDir = -speed;
                
                
            } else if(transform.position.x < locPoint[currentPoint].position.x) {
                if(!isFacingLeft)
                {
                    Flip();
                }
                Flip();
                moveDir = speed;
                
            }
        } else {
            rb.velocity = Vector2.zero;
            currentPoint++;
            if (currentPoint >= locPoint.Length){
                currentPoint = 0;
            }
        }
        rb.velocity = new Vector2(moveDir,rb.velocity.y);
    }
    
    float Distance()
    {
        return Vector3.Distance(transform.position,player.position);
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
        
        if (playerPosition > 0 && isFacingLeft){
            Flip();
        } else if (playerPosition < 0 && !isFacingLeft){
            Flip();
        }
    }
    
    public void IsMoving()
    {
        isMove = true;
    }
    public void NotMoving()
    {
        isMove = false;
    }
    public void IsChasing()
    {
        isChase = true;
    }
    public void NotChasing()
    {
        isChase = false;
    }
    public void isAttacking()
    {
        isAttack = true;
    }
    public void StopAttacking()
    {
        isAttack = false;
        
    }

    public void StopMove()
    {
        rb.velocity = Vector2.zero;
    }
    public void StopKnockBack()
    {
        
        rb.velocity = Vector2.zero;
    }

    
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
        Gizmos.DrawWireSphere(transform.position, aggroAttackDistance);
    }


}
