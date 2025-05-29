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
        //currentTime = timerDuration;
    }

    private void BasketManager_OnBasketMinigameStart(object sender, System.EventArgs e)
    {
        currentTime = timerDuration;
        timerRunning = true;
        Debug.Log("Start Basket timer");
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            Debug.Log(currentTime);

            if (currentTime <= 0f)
            {
                Debug.Log("Basket Timer Finish");
                OnBasketTimerFinish?.Invoke(this, EventArgs.Empty);
            }
        }

        string formattedTime = currentTime.ToString("00.0");
        formattedTime = formattedTime.Replace(",", ":").Replace(".", ":"); 
        timerText.text = formattedTime;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
