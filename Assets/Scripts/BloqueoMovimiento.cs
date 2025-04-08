using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class DisableTeleport : MonoBehaviour
{
    public TeleportationProvider teleportationProvider;
    public ContinuousMoveProvider moveProvider;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            teleportationProvider.enabled = false;  // Desactivar la teletransportación
            moveProvider.enabled = false;
            StartCoroutine(activateMove_Tp());
        }
    }

    private IEnumerator activateMove_Tp()
    {
        yield return new WaitForSeconds(10);
        teleportationProvider.enabled = true; 
        moveProvider.enabled = true;
    }
}
