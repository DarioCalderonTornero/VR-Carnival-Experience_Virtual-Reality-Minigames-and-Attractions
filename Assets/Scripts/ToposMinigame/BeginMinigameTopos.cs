using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class BeginMinigameTopos : MonoBehaviour
{
    public static BeginMinigameTopos Instance { get; private set; }

    public event EventHandler OnBeginMinigameTopos;

    private bool beginMinigame = false;

    //public ContinuousMoveProvider playerMove;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hammer.OnHammerTriggered += Hammer_OnHammerTriggered;
    }

    private void Hammer_OnHammerTriggered()
    {
        beginMinigame = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //&& beginMinigame
        if (other.CompareTag("Player"))
        {
            Debug.Log("StartMinigame");
            OnBeginMinigameTopos?.Invoke(this,EventArgs.Empty);
            //playerMove.enabled = false;
        }
    }
}
