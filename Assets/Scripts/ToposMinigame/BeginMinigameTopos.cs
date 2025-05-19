using System;
using UnityEngine;

public class BeginMinigameTopos : MonoBehaviour
{
    public static BeginMinigameTopos Instance { get; private set; }

    public event EventHandler OnBeginMinigameTopos;

    private bool beginMinigame = false;

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
        if (other.CompareTag("Player") && beginMinigame)
        {
            Debug.Log("StartMinigame");
            OnBeginMinigameTopos?.Invoke(this,EventArgs.Empty);
        }
    }
}
