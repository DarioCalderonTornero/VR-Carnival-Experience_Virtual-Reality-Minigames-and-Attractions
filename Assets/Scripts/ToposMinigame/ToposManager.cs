using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class ToposManager : MonoBehaviour
{
    public static ToposManager Instance { get; private set; }

    [SerializeField] private ContinuousMoveProvider playerMove;
    bool startMinigame = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartToposMinigame()
    {
        startMinigame = true;
        //playerMove.enabled = false;
    }

    public void StopToposMinigame()
    {
        startMinigame = false;
        //playerMove.enabled = true;
    }
}
