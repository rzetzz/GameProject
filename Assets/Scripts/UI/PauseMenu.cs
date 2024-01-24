using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] DataSave data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInputSetting.instance.pause)
        {
            Pause();
        }
    }

    public void Pause()
    {
        data.gamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        
        StartCoroutine(con());
    }

    public IEnumerator con()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        yield return null;
        data.gamePaused = false;
    }
}
