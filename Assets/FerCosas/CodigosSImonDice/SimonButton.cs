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

    [HideInInspector]
    public bool interactableEnabled = true;

    private SimonSaysGame gameManager;
    private SimonSoundManager soundManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<SimonSaysGame>();
        soundManager = FindFirstObjectByType<SimonSoundManager>();

        SetColor(baseColor);

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

        soundManager?.PlayClickSound();
        gameManager?.ReceivePlayerInput(buttonID);
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        SetColor(flashColor);
        soundManager?.PlayColorSound(buttonID);
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
}
