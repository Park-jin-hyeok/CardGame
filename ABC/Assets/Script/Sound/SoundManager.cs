using UnityEngine;

public class SoundManager : ISoundManager
{
    private AudioSource audioSource;
    private AudioClip cardFlipSound;
    private AudioClip timerSound;

    public SoundManager(AudioSource source, AudioClip flipSound, AudioClip timerSound)
    {
        audioSource = source;
        cardFlipSound = flipSound;
        this.timerSound = timerSound;
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
            audioSource.loop = true;  // 타이머 소리는 반복 재생되도록 설정
            audioSource.Play();       // Play() 호출을 통해 소리 재생 시작
        }
    }

    public void StopTimerSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false; // 반복 재생 해제
        }
    }
}
