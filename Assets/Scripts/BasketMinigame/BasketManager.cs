using System;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    public static BasketManager Instance { get; private set; }

    public static event EventHandler OnBasketMinigameStart;

    public bool basketMinigameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartBasketMinigame();
    }

    public void StartBasketMinigame()
    {
        if (!basketMinigameStarted)
        {
            Debug.Log("StartBasketMinigame");
            basketMinigameStarted = true;
            StartCoroutine(CountDown.Instance.Countdown(() =>
            {
                OnBasketMinigameStart?.Invoke(this, EventArgs.Empty);
            }));
        }
    }

    public void ResetMinigame()
    {
        basketMinigameStarted = false;
    }

    public bool IsMinigameStarted()
    {
        return basketMinigameStarted;
    }
}
