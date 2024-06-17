using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;
using Unity.VisualScripting;


public class SoundEffect
{
    public string name; // Name of the sound effect.
    public AudioClip audioClip; // The AudioClip associated with the sound effect.

    public SoundEffect(string name, AudioClip audioClip)
    {
        this.name = name;
        this.audioClip = audioClip;
    }
}

public class MainMusicManager : MonoBehaviour
{

    
    public AudioSource audioSource; // Reference to the AudioSource component.
    public List<SoundEffect> soundEffects = new List<SoundEffect>(); // List to store sound effects.\
    AudioClip selectedSound;

    private void Start()
    {

        // Get a reference to the AudioSource and Button components.
        audioSource = GetComponent<AudioSource>();

    }

    public void PlaySoundEffect(string SoundName)
    {
        // Check if there are sound effects in the list.
        if (soundEffects.Count > 0)
        {
            // Choose a random sound effect from the list.
             selectedSound = ChangeSound(SoundName);

            // Play the selected sound effect.
            audioSource.clip = selectedSound;
            audioSource.Stop();
            audioSource.Play();     
            

        }
    }
    public AudioClip ChangeSound(string SoundName)
    {
        SoundEffect soundEffect;
        if (SoundName == "StreetSound")
        {
            soundEffect = soundEffects.Find(effect => effect.name == SoundName);
            return soundEffect.audioClip ;

        }
        if (SoundName == "BattleMusic")
        {
            soundEffect = soundEffects.Find(effect => effect.name == SoundName);
            return soundEffect.audioClip;

        } 

        return null;
    }

    public void ChangeMainMusic(AudioClip NewMusic)
    {
        // Change the main music to the specified AudioClip.
        audioSource.Stop();
        audioSource.clip = NewMusic;
        audioSource.Play();

    }
}
