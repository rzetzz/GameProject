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
    private float attackCounter;
    private float waitCounter;
    private bool isFacingLeft = true;
    private bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitCounter > 0)
        {
            waitCounter -= Time.deltaTime;
        }
        
        if(waitCounter <= 0)
        {
            if(GetPlayerPosition() && !isAttack)
            {
            FlipTowardsPlayer();
            attackCounter = attackTime;
            isAttack = true;
            }

            if(isAttack)
            {
                if(attackCounter > 0)
                {
                    attackCounter -=Time.deltaTime;
                    rb.velocity = new Vector2(attackPower*-transform.localScale.x,0);
                }
                else
                {
                    isAttack = false;
                    waitCounter = waitTime;
                    rb.velocity = Vector2.zero;
                }
            }

        }
        
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

    float Distance()
    {
        return Vector3.Distance(transform.position,player.position);
    }
    
    private IEnumerator Attack(float time)
    {   
        yield return new WaitForSeconds(0.7f);
        rb.velocity = new Vector2(attackPower*-transform.localScale.x,0);
        yield return new WaitForSeconds(time);
        rb.velocity = Vector2.zero;
        isAttack = false;
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
    
    
    
    
    
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
        Gizmos.DrawWireSphere(transform.position, aggroAttackDistance);
    }


}
