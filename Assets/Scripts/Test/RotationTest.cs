using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    float angle;
    Transform player;
    [SerializeField] float turnSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,getPosition(), turnSpeed * Time.deltaTime);
    }

    Vector2 Direction()
    {
        Vector2 dir = player.position - transform.position;
        dir.Normalize();
        return dir;
    }
    public Quaternion getPosition(){

        angle = Mathf.Atan2(Direction().x, -Direction().y) * Mathf.Rad2Deg; 
        
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        
        return targetRot;

       
    }
}
