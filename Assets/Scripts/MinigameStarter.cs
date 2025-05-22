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
        interactable.selectEntered.AddListener(OnSelected);
    }

    private void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSelected);
    }

    private void OnSelected(SelectEnterEventArgs args)
    {
        CountDown.Instance.StartCoroutine(
            CountDown.Instance.Countdown(() =>
            {
                minigameToStart.GetComponent<BallManager>().StartGame();
            }));
    }
}

