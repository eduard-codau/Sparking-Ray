using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerLobby : MonoBehaviour
{
    public Sound[] sounds;
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
        PlaySoundMenu("Menu");
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMusicOnState(string name)
    {
        if(state == 0)
        {
            state = 1;
            StopSoundMenu(name);
        }
        else
        {
            state = 0;
            PlaySoundMenu(name);
        }


    }
    public void PlaySoundMenu(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Play();
        }
    }

    public void StopSoundMenu(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Stop();
        }
    }
}