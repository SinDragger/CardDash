using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SettingMenu_MainGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenSettingMenu()
    {
        this.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        wyc.GameController.instance.GameScenceChange(0);
        //wyc.GameController.instance.;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SwitchMusic()
    {
        MusicController.instance.SwitchMusic();
    }

}
