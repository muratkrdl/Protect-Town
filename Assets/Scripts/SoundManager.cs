using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    public static float volumeAmount;


    public static void PlaySound(AudioClip audioClip)
    {
        GameObject soundGameObject = new GameObject();

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        soundGameObject.AddComponent<KYS>();
        audioSource.volume = volumeAmount;
        audioSource.PlayOneShot(audioClip);
    }

}
