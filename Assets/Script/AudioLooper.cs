using UnityEngine;

public class AudioLooper : MonoBehaviour {
    public float delay = 0.5f;

    AudioSource source { get { return GetComponent<AudioSource>(); } }

    void FixedUpdate()
    {
        if (source.enabled)
        {
            if (source.isPlaying)
                return;

            source.pitch = Random.Range(0.5f, 1.0f);

            source.PlayDelayed(delay);
        }
    }
}
