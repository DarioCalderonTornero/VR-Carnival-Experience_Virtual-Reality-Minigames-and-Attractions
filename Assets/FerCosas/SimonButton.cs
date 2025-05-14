using UnityEngine;


public class SimonButton : MonoBehaviour
{
    [Header("Referencia al objeto base del bot�n")]
    public Renderer baseRenderer;

    [Header("Color normal y color iluminado")]
    public Color baseColor;
    public Color flashColor = Color.white;

    [Header("ID del bot�n")]
    public int buttonID;

    [HideInInspector]
    public bool interactableEnabled = true;

    private SimonSaysGame gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<SimonSaysGame>();
        SetColor(baseColor);

        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable != null)
            interactable.selectEntered.AddListener(_ => OnPressed());
    }

    public void OnPressed()
    {
        if (!interactableEnabled)
            return;

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

    public void SetColor(Color color)
    {
        if (baseRenderer != null)
            baseRenderer.material.color = color;
    }

    public void SetInteractable(bool value)
    {
        interactableEnabled = value;
    }
}
