using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource BGM;
    float musicVolume;//0~1
    bool active;
    public static MusicController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        active = true;
    }

    public void SwitchMusic()
    {
        if (active)
        {
            TurnOffMusic();
        }
        else
        {
            TurnOnMusic();
        }
    }

    public void TurnOnMusic()
    {
        if (active) return;
        BGM.Play();
        active = true;
    }
    public void TurnOffMusic()
    {
        if (!active) return;
        BGM.Stop();
        active = false;
    }
    public void SetVolume(float volume)
    {
        musicVolume = volume;
        BGM.volume = volume;
        
    }


}
