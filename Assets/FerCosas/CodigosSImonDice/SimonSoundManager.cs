using UnityEngine;

public class SimonSoundManager : MonoBehaviour
{
    [Header("Sonidos por color (ordenados)")]
    public AudioClip[] colorSounds; 

    [Header("Sonido al pulsar botón")]
    public AudioClip clickSound;

    [Header("Sonido de fallo")]
    public AudioClip failSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayColorSound(int id)
    {
        if (id >= 0 && id < colorSounds.Length && colorSounds[id] != null)
        {
            audioSource.PlayOneShot(colorSounds[id]);
        }
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    public void PlayFailSound()
    {
        if (failSound != null)
            audioSource.PlayOneShot(failSound);
    }
}
