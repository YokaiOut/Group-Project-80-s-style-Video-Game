using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    AudioSource audioSource { get { return GetComponent<AudioSource>(); } }
    public AudioClip clip;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (audioSource.enabled)
        {
            audioSource.clip = clip;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (audioSource.enabled)
        {
            audioSource.Stop();
        }
    }
}
