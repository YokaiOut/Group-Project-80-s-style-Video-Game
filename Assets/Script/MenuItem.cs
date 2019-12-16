using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class MenuItem : MonoBehaviour, ISelectHandler
{
    public AudioClip moveMenu;
    public AudioMixerGroup mixerGroup;
    AudioSource SFXSource;

    void Start()
    {
        var audio = gameObject.AddComponent<AudioSource>();
        audio.outputAudioMixerGroup = mixerGroup;
        SFXSource = audio;
    }

    public void OnSelect(BaseEventData data)
    {
        PlaySound(moveMenu);
    }

    public void PlaySound(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }
}
