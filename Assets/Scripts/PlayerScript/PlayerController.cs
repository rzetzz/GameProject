using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 2;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float jumpMultiplier = 2;
    [SerializeField] private float jumpTime;
    private float jumpTemp = 1f;
    private float jumpCounter;
    private bool isJumping;
    private bool canDoubleJump;
    private float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;
    private float currentJumpM;
    
    [Header("Wall Jump")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallSlide;
    private bool isWallJump;
    private float wallJumpDirection;
    private float wallSlidingSpeed = 2f;
    [SerializeField]private float wallJumpDuration = 1f;
    [SerializeField]private Vector2 wallJumpPower;
    private float wallJumpCounter;
    Vector2 jumpDir;
    bool hasDir;
    
    [Header("Dash")]
    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashTime = 2f;
    private bool isDashing;
    private bool canDash = true;

    [Header("Charge Dash")]
    [SerializeField] private float chargeDashPower = 10f;
    [SerializeField] private float chargeDashCooldownTime = 1f;
    [SerializeField] private GameObject chargeEffect;
    private float chargeDashCounter;
    private bool isChargeDash;
    private bool canChargeDash;
    private bool isCharging;
    private float chargeDir;
    
    [Header("Attack")]
    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject attackFinal;
    [SerializeField] private GameObject attackUp;
    [SerializeField] private GameObject attackDown;
    private bool isAttack;
    private bool isFinalAttack;
    private int attackState = 0;
    [SerializeField]private float attackDistance = 0.5f;
    private float attackDistanceCounter;
    private bool canAttack = true;
    [SerializeField]private float attackCooldown;
    [SerializeField]private float chainAttackTime;
    private float chainAttackCounter;
    private float attackCooldownCounter;
    private bool isMovingAttack;
    [SerializeField] private float attackBufferTime = 0.2f;
    private float attackBufferCounter;
    private float attackDir;
    
    [Header("Bow Attack")]
    [SerializeField] GameObject theBow;
    public bool isBowAttack;
    [SerializeField] float bowAirTime = 1f;
    [SerializeField] Transform bowLocation;
    private float bowAirCounter;
    
    [Header("Other")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    
    
    private float oriGravity;
    Vector2 vecGravity;
    public bool isFacingRight = true;
    Rigidbody2D playerRb;
    PlayerAbilities abilities;
    Animator setAnim;
    public TrailRenderer trail;
    float flipCooldown = 0.03f;
    float flipCooldownCounter;
    float horizontal;
    
    // Start is called before the first frame update
    void Start()
    {   
        abilities = GetComponent<PlayerAbilities>();
        playerRb = GetComponent<Rigidbody2D>();
        setAnim = GetComponent<Animator>();
        vecGravity = new Vector2(0,-Physics2D.gravity.y);
        oriGravity = playerRb.gravityScale;
        bowAirCounter = bowAirTime;
       
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = MoveInput();
       

        if (isDashing)
        {
            isJumping = false; 
        }
        // Movement
        if (!isDashing && !isCharging && !isChargeDash && !isMovingAttack && !isBowAttack)
        {

            playerRb.velocity = new Vector2(moveSpeed * horizontal,playerRb.velocity.y);
        }
        
        
        //Jump
        if (isGrounded() && !PlayerInputSetting.instance.jumpClick)
        {
            canDoubleJump = false;  
        }
        if(isGrounded() && !isJumping)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x,0f);
        }
        
        if (isGrounded())
        {
            
            isWallJump = false;
            canDash = true;
            coyoteTimeCounter = coyoteTime;
            jumpTemp = 1f;
            flipCooldownCounter = 0;
            
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(PlayerInputSetting.instance.jumpClick)
        {
            chainAttackCounter = 0;
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if(isWallSlide)
        {
            canDoubleJump = false;
            canDash = true;
        }
        if(!isGrounded() && jumpTemp > 0 && !isWallSlide && coyoteTimeCounter <= 0)
        {
            canDoubleJump = true;
        }

        if (jumpBufferCounter > 0 && !isDashing)
        {
            if (coyoteTimeCounter > 0 || (canDoubleJump && abilities.canDoubleJump))
            {
                
                jumpTemp -= 1;
                canDoubleJump = !canDoubleJump; 
                playerRb.velocity = new Vector2(playerRb.velocity.x,jumpForce);
                isJumping = true;
                jumpCounter = 0;
                jumpBufferCounter = 0f;
            }  
        }
        
        if(playerRb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime)
            {
                isJumping = false;
            }

            float t = jumpCounter / jumpTime;
            currentJumpM = jumpMultiplier;

            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1-t);
            }
            
        }
        if (PlayerInputSetting.instance.jumpRelease)
        {
            isJumping = false;
            jumpCounter = 0;
            coyoteTimeCounter = 0;

            if(playerRb.velocity.y > 0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * 0.5f);
            }
        }
        if(playerRb.velocity.y < 0)
        {
            playerRb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }

        

        //Dash
        if (PlayerInputSetting.instance.dashClick && (canDash && abilities.canDash) && !isMovingAttack)
        {
            setAnim.SetTrigger("isDashing");
            StartCoroutine(Dash());
            
        }
        //Flip
        if (horizontal < 0 && isFacingRight && flipCooldownCounter <= 0 && !isMovingAttack )
        {
            Flip();
        }
        else if (horizontal > 0 && !isFacingRight && flipCooldownCounter <= 0 && !isMovingAttack )
        {
            Flip();
            
        }
        if(abilities.canWallJump)
        {
            WallSlide();
            WallJump();
        }
        if(abilities.canChargeDash)
        {
            ChargeDash();
        } 
        if(abilities.canAttackBow)
        {
            BowAttack();
        }
        
        if(playerRb.velocity.y > 0)
        {
            setAnim.SetBool("isJumping",true);
            setAnim.SetBool("isFalling",false);
            setAnim.SetBool("isLanding",false);
        }
        else
        {
            setAnim.SetBool("isJumping",false);
        }
        if(playerRb.velocity.y < 0)
        {
            setAnim.SetBool("isJumping",false);
            setAnim.SetBool("isFalling",true);
            setAnim.SetBool("isLanding",false);
            
            
        }
        
        if(playerRb.velocity.y == 0 && isGrounded())
        {
            setAnim.SetBool("isFalling",false);
            setAnim.SetBool("isLanding",true);
        }
        
        Attack();
        
        setAnim.SetBool("isGrounded",isGrounded());
        setAnim.SetInteger("AttackState",attackState);
        setAnim.SetBool("isWallSliding",isWallSlide);
        setAnim.SetFloat("Movement",Mathf.Abs(playerRb.velocity.x));
    }

    private void FixedUpdate() {
        if(playerRb.velocity.y > 0 && isJumping)
        {
            playerRb.velocity += vecGravity * currentJumpM *Time.deltaTime;
        }
    }
    float MoveInput()
    {
        if(PlayerInputSetting.instance.moveDir.x > 0)
        {
            return 1;
        } 
        else if (PlayerInputSetting.instance.moveDir.x < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
    }
    bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position,groundCheckRadius,wallLayer);
    }
    void ChargeDash()
    {
        
        if(!isDashing)
        {
            if (isGrounded())
            {
                if(PlayerInputSetting.instance.chargeDashClick)
                {
                    chargeDashCounter = chargeDashCooldownTime;
                }
                if(PlayerInputSetting.instance.chargeDash && chargeDashCounter > 0)
                { 
                    playerRb.velocity = Vector2.zero;
                    isCharging = true;
                    chargeEffect.SetActive(true);
                    setAnim.SetBool("isCharging",true);
                    chargeDashCounter -= Time.deltaTime;
                }
                if (chargeDashCounter <= 0 && isCharging)
                {
                    setAnim.SetBool("isCharging",false);
                    chargeEffect.SetActive(false);
                    setAnim.SetBool("isChargingFull",true);
                    canChargeDash = true;
                }
                else
                {   
                    setAnim.SetBool("isChargingFull",false);
                    canChargeDash = false;
                }
                if(PlayerInputSetting.instance.chargeDashRelease)
                {
                    if(canChargeDash)
                    {
                        
                        chargeDir = transform.localScale.x;
                        isCharging = false;
                        isChargeDash = true;
                        canChargeDash = false;   
                    }
                    else
                    {
                        chargeEffect.SetActive(false);
                        setAnim.SetBool("isCharging",false);
                        isCharging = false;
                        isChargeDash = false;
                    }  
                }
            }
            if (isChargeDash)
            {
                
                setAnim.SetBool("isChargingFull",false);
                playerRb.velocity = new Vector2(chargeDashPower * chargeDir,playerRb.velocity.y);
                trail.emitting = true;
            }
            else
            {
                canChargeDash = false;
                trail.emitting = false;
            }
            
            if(isChargeDash && (horizontal == -chargeDir || isWalled()))
            {
                
                isChargeDash = false;
            }
            
            setAnim.SetBool("isChargeDashing",isChargeDash);

        }
        
    }
    void WallSlide()
    {
        if (isWalled() && !isGrounded() && !PlayerInputSetting.instance.jump)
        {
            isWallSlide = true;   
        }
        
        if (isGrounded() || (!isGrounded() && !isWalled()))
        {
            isWallSlide = false;
        }
        if(isWallSlide && MoveInput() != 0)
            {
                
                playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Clamp(playerRb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            }
    }
    void WallJump()
    {
        if (isWallSlide)
        {
            
            isWallJump = false;
            flipCooldownCounter = flipCooldown;
            wallJumpDirection = -transform.localScale.x;
            
            wallJumpCounter = wallJumpDuration;
        }
        else
        {
            flipCooldownCounter -= Time.deltaTime;
            wallJumpCounter -= Time.deltaTime;
        }
    
        if (PlayerInputSetting.instance.jumpClick && wallJumpCounter > 0)
        {
            isWallJump = true;
                           
        }
        if (isWallJump)
        {
            
            
            if (!hasDir){
                    Flip();
                    hasDir = true;
            }
            
            canDoubleJump = true;
            float jumpSwitch = wallJumpDuration * 0.2f;
            
            if(wallJumpCounter < jumpSwitch)
            {            
                playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            }
            else if(wallJumpCounter > jumpSwitch)
            {
                playerRb.velocity = new Vector2(wallJumpPower.x * wallJumpDirection, wallJumpPower.y); 
            }
        }
        if (!PlayerInputSetting.instance.jump || wallJumpCounter <0)
        {
            wallJumpCounter = 0f;
            isWallJump = false;
            hasDir = false;
        }
        if(isGrounded())
        {
            wallJumpCounter = 0;
        }
        
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
    void Attack()
    {
        // if(Input.GetButtonDown("Fire1"))
        // {
        //     attackState += 1;
        //     attackBufferCounter = attackBufferTime;
        // }
        // else
        // {
        //     attackBufferCounter -= Time.deltaTime;
        // }
        if(PlayerInputSetting.instance.attackClick && canAttack)
        {   
            setAnim.SetTrigger("Attack");
            attackDir = transform.localScale.x;
            attackState += 1;
            isAttack = true;
            canAttack = false;
            chainAttackCounter = chainAttackTime;
            attackDistanceCounter = attackDistance;
            attackCooldownCounter = attackCooldown;
            if(PlayerInputSetting.instance.moveDir.y <0.8 && PlayerInputSetting.instance.moveDir.y > -0.8)
            {
                StartCoroutine(AttackDistance());
            }
            else
            {
                playerRb.velocity = Vector2.zero;
            }
            
            if(attackState > 3)
            {
                
                attackState = 1;
            }
            
        }
        if(PlayerInputSetting.instance.moveDir.y > 0.8f)
        {
            setAnim.SetBool("isLookingUp",true);
        }
        else
        {
            setAnim.SetBool("isLookingUp",false);
        }

        if(PlayerInputSetting.instance.moveDir.y < -0.8f)
        {
            setAnim.SetBool("isLookingDown",true);
        }
        else
        {
            setAnim.SetBool("isLookingDown",false);
        }
        
        if(isAttack)
        {
            if(chainAttackCounter <= chainAttackTime * 0.5f)
            {
                playerRb.gravityScale = oriGravity;
            }
            else
            {
                playerRb.gravityScale = 0f;
            }
        }

        if(chainAttackCounter <= 0)
        {
            isAttack = false;
            attackState = 0;
        }
        else
        {
            
            chainAttackCounter -= Time.deltaTime;
        }
        

        
    }
    void BowAttack()
    {
        if (PlayerInputSetting.instance.bowAttack && bowAirCounter > 0)
        {
            if(!isGrounded())
            {
                bowAirCounter -= Time.deltaTime;
            }
            
            playerRb.velocity = Vector2.zero;
            canAttack = false;
            canDash = false;
            isBowAttack = true;
            playerRb.gravityScale = 2f;
            
            theBow.transform.position = bowLocation.position;
            theBow.transform.SetParent(null);
            
            theBow.SetActive(true);
        }
        if(bowAirCounter <= 0)
        {
            isBowAttack = false;
            theBow.SetActive(false);
        }
        if (PlayerInputSetting.instance.bowRelease) 
        {
            
            playerRb.gravityScale = oriGravity;
            canAttack = true;
            canDash = true;
            isBowAttack = false;
            theBow.transform.SetParent(this.transform);
            theBow.transform.position = bowLocation.position;
            theBow.transform.localScale = bowLocation.localScale;
            theBow.transform.rotation = Quaternion.identity;
            theBow.SetActive(false);
            
        }

        if (!PlayerInputSetting.instance.bowAttack)
        {
            bowAirCounter = bowAirTime;
        }
        setAnim.SetBool("isBowAttack",isBowAttack);
    }
    //Animation Event
    
    void SetAttack()
    {
         attack.SetActive(true);
    } 
    void SetFinalAttack()
    {
        attackFinal.SetActive(true); 
    }
    void SetAttackUp()
    {
         attackUp.SetActive(true);
    } 
    void SetAttackDown()
    {
         attackDown.SetActive(true);
    } 
    void SetStateAttack()
    {
        attackState = 0;
    }
    void AttackYes()
    {
        canAttack = true;
    }
    void AttackNo()
    {
        canAttack = false;
    }

    void SetMoving()
    {
        isMovingAttack = true;
    }
    void SetStoped()
    {
        isMovingAttack = false;
    }
    private IEnumerator Dash()
    {
        isAttack = false;
        
        trail.emitting =true;
        canDash = false;
        isDashing = true;   
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(transform.localScale.x * dashPower,0f);

        yield return new WaitForSeconds(dashTime);
        
        trail.emitting =false;
        isDashing = false;
        playerRb.gravityScale = oriGravity;

    }

    private IEnumerator AttackDistance()
    {
        // isMovingAttack = true;
        
       
        
        playerRb.velocity = new Vector2(moveSpeed * attackDir,0f);
        yield return new WaitForSeconds(attackCooldown);
        playerRb.velocity = Vector2.zero;
        
        
    }

   private void OnDrawGizmos()
    {
        // Mengatur warna gizmo
        Gizmos.color = Color.red;

        // Menggambar wire sphere (lingkaran) berdasarkan posisi wallCheck dan groundCheckRadius
        Gizmos.DrawWireSphere(wallCheck.position, groundCheckRadius);
    }
}
