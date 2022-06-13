using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private int state;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
        state = 0;
        //PlaySound("Main_theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if(s.name == name)
                s.source.Play();
        }
    }

    public void StopSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Stop();
        }
    }

    public void ChangeMusicOnState(string name)
    {
        if (state == 0)
        {
            state = 1;
            StopSound(name);
        }
        else
        {
            state = 0;
            PlaySound(name);
        }
    }
}