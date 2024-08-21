using UnityEngine;

public class SoundManager : ISoundManager
{
    private AudioSource audioSource;
    private AudioClip cardFlipSound;
    private AudioClip timerSound;
    private AudioClip correctSound; 

    public SoundManager(AudioSource source, AudioClip flipSound, AudioClip timerSound, AudioClip correctSound)
    {
        audioSource = source;
        cardFlipSound = flipSound;
        this.timerSound = timerSound;
        this.correctSound = correctSound;
    }

    public void PlayCardFlipSound()
    {
        if (audioSource != null && cardFlipSound != null)
        {
            audioSource.PlayOneShot(cardFlipSound);
        }
    }

    public void PlayTimerSound()
    {
        if (audioSource != null && timerSound != null)
        {
            audioSource.clip = timerSound;
            audioSource.loop = true;  
            audioSource.Play();       
        }
    }

    public void StopTimerSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false; 
        }
    }

    public void PlayCorrectSound()
    {
        if (audioSource != null && correctSound != null)
        {
            audioSource.PlayOneShot(correctSound);
        }
    }
}
