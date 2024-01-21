using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera camTarget;
    Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            camTarget.Priority = 99;
        }
    }
}
