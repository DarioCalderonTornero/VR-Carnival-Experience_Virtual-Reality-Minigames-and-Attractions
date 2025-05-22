using System;
using UnityEngine;

public class BeginMiniGame : MonoBehaviour
{
    public static BeginMiniGame Instance { get; private set; }

    public event EventHandler OnBeginBaseBall;

    public bool started = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !started)
        {
            BallManager.Instance.StartGame();
        }
    }
}
