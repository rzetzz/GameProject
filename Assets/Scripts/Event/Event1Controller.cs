using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Event1Controller : MonoBehaviour
{
    [SerializeField] Animator setDoor;
    [SerializeField] GameObject[] kuku;
    [SerializeField] GameObject[] spawn;
    [SerializeField] float waitTime = 5f;
    float waitCounter;
    bool eventStart;
    [SerializeField] CinemachineVirtualCamera camTarget;
    bool firstWave, secondWave, thirdWave,waveStart;
    Collider2D coll;
    [SerializeField] DataSave data;
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(!data.event1)
        {
            if(eventStart)
            {   
                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                if (!secondWave && !thirdWave && waveStart)
                {
                    
                    StartCoroutine(FirstWave(2));
                    waveStart = false;
                }
                if ((kuku[0].GetComponent<EnemyBehaviour>().isDead && kuku[1].GetComponent<EnemyBehaviour>().isDead || waitCounter <= 0 ) && firstWave)
                {
                    StartCoroutine(SecondWave(2));
                    waitCounter = waitTime;
                    firstWave = false;
                }
                if ((kuku[2].GetComponent<EnemyBehaviour>().isDead && kuku[3].GetComponent<EnemyBehaviour>().isDead || waitCounter <= 0 ) && secondWave)
                {
                    StartCoroutine(ThirdWave(2));
                    waitCounter = waitTime;
                    secondWave = false;
                }
                if (kuku[4].GetComponent<EnemyBehaviour>().isDead && kuku[5].GetComponent<EnemyBehaviour>().isDead && kuku[6].GetComponent<EnemyBehaviour>().isDead && kuku[7].GetComponent<EnemyBehaviour>().isDead && !firstWave && !secondWave)
                {
                    thirdWave = false;
                    camTarget.Priority = 0;
                    data.event1 = true;
                    setDoor.SetTrigger("End");
                }
            }
        } 
        else
        {
            coll.enabled = false;
        }   
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            setDoor.SetTrigger("Start");
            waitCounter = waitTime;
            eventStart = true;
            waveStart = true;
            coll.enabled = false;
        }
    }

    IEnumerator FirstWave(int sec)
    {
        spawn[0].SetActive(true);
        spawn[1].SetActive(true);
        yield return new WaitForSeconds(sec);
        spawn[0].SetActive(false);
        spawn[1].SetActive(false);
        firstWave = true;
        
        kuku[0].SetActive(true);
        kuku[1].SetActive(true);
    }

    IEnumerator SecondWave(int sec)
    {
        spawn[2].SetActive(true);
        spawn[3].SetActive(true);
        yield return new WaitForSeconds(sec);
        spawn[2].SetActive(false);
        spawn[3].SetActive(false);
        secondWave = true;
        kuku[2].SetActive(true);
        kuku[3].SetActive(true);
    }

    IEnumerator ThirdWave(int sec)
    {
        spawn[4].SetActive(true);
        spawn[5].SetActive(true);
        spawn[6].SetActive(true);
        spawn[7].SetActive(true);
        yield return new WaitForSeconds(sec);
        spawn[4].SetActive(false);
        spawn[5].SetActive(false);
        spawn[6].SetActive(false);
        spawn[7].SetActive(false);
        thirdWave = true;
        kuku[4].SetActive(true);
        kuku[5].SetActive(true);
        kuku[6].SetActive(true);
        kuku[7].SetActive(true);
    }
}
