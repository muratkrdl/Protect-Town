using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void PlayClickSound()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }

}
