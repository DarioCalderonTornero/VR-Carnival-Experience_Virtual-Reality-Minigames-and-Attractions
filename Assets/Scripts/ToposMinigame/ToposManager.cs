using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class ToposManager : MonoBehaviour
{
    [SerializeField] private ContinuousMoveProvider playerMove;
    bool startMinigame = false;

    public void StartToposMinigame()
    {
        startMinigame = true;
        playerMove.enabled = false;
    }

    public void StopToposMinigame()
    {
        startMinigame = false;
        playerMove.enabled = true;
    }
}
