using System;
using TMPro;
using UnityEngine;

public class BasketTimer : MonoBehaviour
{

    [SerializeField] private float currentTime;
    [SerializeField] private float timerDuration = 60;
    [SerializeField] private bool timerRunning = true;
    [SerializeField] private TextMeshProUGUI timerText;

    public static event EventHandler OnBasketTimerFinish;

    private void Start()
    {
        BasketManager.OnBasketMinigameStart += BasketManager_OnBasketMinigameStart;
        PeriodManager.Instance.OnGameFinish += PeriodManager_OnGameFinish;
        //currentTime = timerDuration;
    }

    private void PeriodManager_OnGameFinish(object sender, EventArgs e)
    {
        timerRunning = false;
        currentTime = 0f;
    }

    private void BasketManager_OnBasketMinigameStart(object sender, System.EventArgs e)
    {
        currentTime = timerDuration;
        timerRunning = true;
        Debug.Log("Start Basket timer");
    }

    private void Update()
    {
        string formattedTime = currentTime.ToString("00.0");
        formattedTime = formattedTime.Replace(",", ":").Replace(".", ":");
        timerText.text = formattedTime;

        if (!timerRunning)
            return;

        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            Debug.Log(currentTime);

            if (currentTime <= 0f)
            {
                timerRunning = false;
                currentTime = 0f;
                OnBasketTimerFinish?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
