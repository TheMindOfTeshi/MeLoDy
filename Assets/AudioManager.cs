
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;  // Sound Array for all the Gamesounds
    
    public static AudioManager instance;
    void Awake()
    {

        if(instance == null){                       // Checks for an instance of the AudioManager in the scene if there is no instance it's set to this instance.
            instance = this;                        
    }
        else{                                       
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);                          // Continues to play even though scenes are swapped

        foreach(Sound s in sounds){                             // Each Sound in the Sound array gets his components added like volume.
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    void Start(){
        Play("Theme");
    }

    public void Play(string name){                              // method to search for a sound in the array by name and then play it.
        Sound s = Array.Find(sounds, sound => sound.name == name);   
        if(s == null){
        
            return;
        }         
        s.source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
