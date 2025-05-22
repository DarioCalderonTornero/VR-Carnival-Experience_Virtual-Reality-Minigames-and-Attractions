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
            OnBeginBaseBall?.Invoke(this, EventArgs.Empty);
            Debug.Log("Begin BaseBall");
            BallManager.Instance.StartGame();
            started = true;
        }
    }
}
