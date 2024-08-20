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
            audioSource.loop = true;  // Ÿ�̸� �Ҹ��� �ݺ� ����ǵ��� ����
            audioSource.Play();       // Play() ȣ���� ���� �Ҹ� ��� ����
        }
    }

    public void StopTimerSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false; // �ݺ� ��� ����
        }
    }
}
