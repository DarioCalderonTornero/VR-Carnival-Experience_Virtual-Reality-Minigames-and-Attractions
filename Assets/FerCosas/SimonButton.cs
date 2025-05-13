using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SimonButton : MonoBehaviour
{
    [Header("Referencia al objeto base del botón")]
    public Renderer baseRenderer;

    [Header("Color normal y color iluminado")]
    public Color baseColor;
    public Color flashColor = Color.white;

    [Header("ID del botón")]
    public int buttonID;

    private SimonSaysGame gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<SimonSaysGame>();
        SetColor(baseColor);

        var interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(_ => OnPressed());
    }

    public void OnPressed()
    {
        gameManager?.ReceivePlayerInput(buttonID);
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        SetColor(flashColor);
        yield return new WaitForSeconds(0.5f);
        SetColor(baseColor);
    }

    private void SetColor(Color color)
    {
        if (baseRenderer != null)
        {
            baseRenderer.material.color = color;
        }
    }
}
