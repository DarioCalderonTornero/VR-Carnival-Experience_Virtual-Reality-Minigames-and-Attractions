using System;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    public static event EventHandler OnBasketMinigameStart;

    private bool ballGrabbed = false;
    private bool basketMinigameStarted = false;

    private void Start()
    {
        StartBasketMinigame();
    }

    public void StartBasketMinigame()
    {
        if (!ballGrabbed)
        {
            Debug.Log("StartBasketMinigame");
            ballGrabbed = true;
            basketMinigameStarted = true;
            StartCoroutine(CountDown.Instance.Countdown(() =>
            {
                OnBasketMinigameStart?.Invoke(this, EventArgs.Empty);
            }));
        }
    }
}
