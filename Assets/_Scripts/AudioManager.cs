﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    public Sound[] sounds;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master")[0];
            s.source.clip = s.clip;
            s.source.volume = s.volume;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start(){
        Play("theme");
    }

    public void Play (string name){

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (name=="dropSound"){
            s.source.PlayDelayed(0.4f);

        }
        else{
            s.source.Play();
        }
    }
}
