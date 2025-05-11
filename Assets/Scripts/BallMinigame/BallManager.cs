using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class BallManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ContinuousMoveProvider continuousMoveProvider;
    [SerializeField] private bool gameStarted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Ball detected");
            continuousMoveProvider.enabled = false;
            gameStarted = true;
        }
    }
}
