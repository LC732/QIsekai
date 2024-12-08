using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEFManager : MonoBehaviour
{
    public static SoundEFManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake(){
        if(instance == null) instance = this;
    }

    public void PlaySoundFXclip(AudioClip audioClip, Transform spawnT, float volume){
        AudioSource audioSource = Instantiate(soundFXObject, spawnT.position, Quaternion.identity);
    
        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    
    }

}
