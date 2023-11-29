using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int sceneBuildIndex;
    [SerializeField] int spawnLoc;
    Animator setTransition;
    [SerializeField]DataSave data;
    // Start is called before the first frame update
    void Start()
    {
        setTransition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            data.sceneIndex = spawnLoc;
            StartCoroutine(NextLevel());
        }
    }
    IEnumerator NextLevel()
    {
        setTransition.SetTrigger("end");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneBuildIndex,LoadSceneMode.Single);
    }
}
