using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip[] audioClips; 

    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }
}
