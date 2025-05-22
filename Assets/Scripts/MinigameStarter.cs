using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MinigameStarter : MonoBehaviour
{
    [SerializeField] private GameObject minigameToStart;

    private XRBaseInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.activated.AddListener(OnActivated);
    }

    private void OnDestroy()
    {
        interactable.activated.RemoveListener(OnActivated);
    }

    private void OnActivated(ActivateEventArgs args)
    {
        BallManager ballManager = minigameToStart.GetComponent<BallManager>();

        if (!ballManager.IsGameActive())
        {
            CountDown.Instance.StartCoroutine(
                CountDown.Instance.Countdown(() =>
                {
                    ballManager.StartGame();
                }));
        }
        else
        {
            Debug.Log("El minijuego ya está en curso. Espera a que termine.");
        }
    }
}
