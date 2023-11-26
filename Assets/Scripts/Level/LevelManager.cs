using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] int sceneBuildIndex;
    [SerializeField] int scene;

    [SerializeField]DataSave data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            data.sceneIndex = scene;
            SceneManager.LoadScene(sceneBuildIndex,LoadSceneMode.Single);
        }
    }
}
