using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonBaseColorChanger : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Renderer baseRenderer;

    [Header("Colores")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color pressedColor = Color.green;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnPressed);
            interactable.selectExited.AddListener(OnReleased);
        }

        if (baseRenderer != null)
            baseRenderer.material.color = defaultColor;
    }

    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnPressed);
            interactable.selectExited.RemoveListener(OnReleased);
        }
    }

    void OnPressed(SelectEnterEventArgs args)
    {
        if (baseRenderer != null)
            baseRenderer.material.color = pressedColor;
    }

    void OnReleased(SelectExitEventArgs args)
    {
        if (baseRenderer != null)
            baseRenderer.material.color = defaultColor;
    }
}
