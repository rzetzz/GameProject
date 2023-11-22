using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputSetting : MonoBehaviour
{
    public static PlayerInputSetting instance;
    
    public float horizontal;
    public Vector2 moveDir;
    public Vector2 axis;
    Vector2 aim;
    public bool jump;
    public bool attack;
    public bool jumpClick;
    public bool jumpRelease;
    public bool dashClick;
    public bool attackClick;
    public bool chargeDash;
    public bool chargeDashClick;
    public bool chargeDashRelease;
    public bool attackRelease;
    public bool bowAttack;
    public bool bowRelease;
    int count;
    PlayerInput playerInput;
    Vector3 mousePos;
    Vector3 mouseAim;
    Vector3 theMouse;
    public Transform lightning;

    void Jump(bool cond){
        jump = cond;
       
    }
    void Attack(bool cond){
        attack = cond;
       
    }
    void Movement(Vector2 direction){
        moveDir = direction;
    }
    void Axis(Vector2 direction){
        aim = direction;
    }
    private void Awake() {
        instance = this;
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update() {
        theMouse = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Camera.main.transform.position.z);
        
        mousePos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 temps = mousePos;
        mousePos.z = Camera.main.nearClipPlane;
        mouseAim = mousePos - transform.position;
        
        mouseAim = new Vector2(theMouse.x - mousePos.x, theMouse.y - mousePos.y);
        mouseAim.Normalize();
        var jumper = playerInput.actions["Jump"];
        jumpClick = jumper.WasPerformedThisFrame();
        jumpRelease = jumper.WasReleasedThisFrame();

        var attack = playerInput.actions["Attack"];
        attackClick = attack.WasPerformedThisFrame();
        attackRelease = attack.WasReleasedThisFrame();

        var dash = playerInput.actions["Dash"];
        dashClick = dash.WasPerformedThisFrame();
        
        var charging = playerInput.actions["Charge"];
        chargeDashClick = charging.WasPerformedThisFrame();
        chargeDashRelease = charging.WasReleasedThisFrame();

        var bow = playerInput.actions["BowAttack"];
        bowRelease = bow.WasReleasedThisFrame();
        
        if(playerInput.currentControlScheme.Equals("Keyboard&Mouse")){
            
            axis = new Vector2(mouseAim.x, mouseAim.y);
        } else {
            axis = aim;
        }

        if(jumpClick){
            count++;
        }
        
       
        
       
        
    }

    public void Aiming(InputAction.CallbackContext ctx){
        Axis(ctx.ReadValue<Vector2>());
        
    }
    public void Moving(InputAction.CallbackContext ctx){
        Movement(ctx.ReadValue<Vector2>());
    }
    public void Jumping(InputAction.CallbackContext ctx){
        if(ctx.performed){
            Jump(true);
        } else if (ctx.canceled){
            Jump(false);
        }
    }

    public void Charging(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            chargeDash = true;
        }
        else if(ctx.canceled)
        {
            chargeDash = false;
        }
    }

    public void BowAim(InputAction.CallbackContext ctx) 
    {
        if(ctx.performed)
        {
            bowAttack = true;
        }
        else if (ctx.canceled)
        {
            bowAttack = false;
        }
    }
    public void Attacking(InputAction.CallbackContext ctx){
        if(ctx.performed){
            Attack(true);
        } else if(ctx.canceled){
            Attack(false);
        }
    }
    
}
