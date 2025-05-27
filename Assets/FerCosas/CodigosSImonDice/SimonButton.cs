using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SimonButton : MonoBehaviour
{
    [Header("Visual")]
    public Renderer baseRenderer;
    public Color baseColor;
    public Color flashColor = Color.white;

    [Header("Identificador")]
    public int buttonID;

    [Header("Audio")]
    public AudioClip soundClip; // sonido propio del botón

    [HideInInspector]
    public bool interactableEnabled = true;

    private SimonSaysGame gameManager;
    private AudioSource audioSource;

    void Start()
    {
        gameManager = FindFirstObjectByType<SimonSaysGame>();

        SetColor(baseColor);

        // Configurar el AudioSource local en 3D
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f;   // 3D
        audioSource.minDistance = 0.5f;
        audioSource.maxDistance = 10f;

        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(_ => OnPressed());
        }
    }

    public void OnPressed()
    {
        if (!interactableEnabled)
            return;

        PlayClickSound();
        gameManager?.ReceivePlayerInput(buttonID);
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        SetColor(flashColor);
        PlayColorSound();
        yield return new WaitForSeconds(0.5f);
        SetColor(baseColor);
    }

    public void SetColor(Color color)
    {
        if (baseRenderer != null)
        {
            baseRenderer.material.color = color;
        }
    }

    public void SetInteractable(bool value)
    {
        interactableEnabled = value;
    }

    private void PlayClickSound()
    {
        if (audioSource != null && soundClip != null)
            audioSource.PlayOneShot(soundClip);
    }

    private void PlayColorSound()
    {
        if (audioSource != null && soundClip != null)
            audioSource.PlayOneShot(soundClip);
    }
}
