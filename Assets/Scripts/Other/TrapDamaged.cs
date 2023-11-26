using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TrapDamaged : MonoBehaviour
{
    
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Transform spawn;
    [SerializeField] Transform spawn2;
    [SerializeField] DataSave data;
    Transform player;
    bool hasSpawn;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(data.sceneIndex == 1)
        {
            player.position = spawn.position;
        }
        else if(data.sceneIndex == 2)
        {
            player.position = spawn2.position;
        }
        cam.Follow = player;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        
    }
    
}
