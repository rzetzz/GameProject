using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawn;
    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(playerPrefab,spawn.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
