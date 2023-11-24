using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{

    [SerializeField] private float turnSpeed=1f;
    private PlayerController player;
    [SerializeField] private Transform objectParent;
    float angle;
    private Animator setAnim;
    public bool launchArrow;
    public float launchSpeed = 1f;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject arrowAttack;

    [SerializeField] GameObject thePoint;
    GameObject[] points;
    [SerializeField]int numberOfPoint;
    [SerializeField] Transform pointParent;

    Vector2 theVelocity;
    void Start()
    {
        points = new GameObject[numberOfPoint];
        for (int i = 0; i < numberOfPoint; i++)
        {
            points[i] = Instantiate(thePoint,transform.position,Quaternion.identity,pointParent);
            points[i].SetActive(false);

        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        setAnim = GetComponent<Animator>();
    }

    private void OnDisable() 
    {
        HideTrajectory();
    }
    

    void Update()
    {
        theVelocity = transform.right*transform.localScale.x * launchSpeed;
        if(PlayerInputSetting.instance.axis.x != 0 || PlayerInputSetting.instance.axis.x != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,getPosition(), turnSpeed * Time.deltaTime);
        }
        if(player.isBowAttack)
        {
            DisplayTrajectory();
        }
      
        
        if(PlayerInputSetting.instance.attack)
        {
            
            arrow.SetActive(true);
        }
        if(PlayerInputSetting.instance.attackRelease)
        {   
            arrow.SetActive(false);
            launchArrow = true;
            arrowAttack.SetActive(true);
            arrowAttack.GetComponent<Rigidbody2D>().velocity = theVelocity;
            
            
        }
        
        
        setAnim.SetBool("isAttack",PlayerInputSetting.instance.attack);
        
        // transform.localScale = transform.parent.localScale;
        
    }
    public Quaternion getPosition(){

        
        if(transform.localScale.x > 0f)
        {
            angle = Mathf.Atan2(PlayerInputSetting.instance.axis.y, PlayerInputSetting.instance.axis.x) * Mathf.Rad2Deg;
        }
        else if (transform.localScale.x < 0f)
        {
            angle = Mathf.Atan2(-PlayerInputSetting.instance.axis.y, -PlayerInputSetting.instance.axis.x) * Mathf.Rad2Deg;
        }
        
        
        
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
        return targetRot;

        
        
       
    }

    Vector2 PointPosition(float t)
    {
        return (Vector2)transform.position + (theVelocity * t) + 0.5f * Physics2D.gravity * (t*t);
    }

    void DisplayTrajectory()
    {
        for (int i = 0; i < points.Length; i++)
        {
           if (i!=0)
           {
               points[i].transform.position = PointPosition(i * 0.05f);
               points[i].SetActive(true);
           }
              
            
        }
    }
    void HideTrajectory()
    {
        for (int i = 0; i < points.Length; i++)
        {
           if (i!=0)
           {
               
               points[i].SetActive(false);
           }
              
            
        }
    }

    float Direction()
    {
        return PlayerInputSetting.instance.axis.y;
    }
}
