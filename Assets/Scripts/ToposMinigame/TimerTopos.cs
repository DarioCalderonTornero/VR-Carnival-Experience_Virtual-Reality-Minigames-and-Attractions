using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerTopos : MonoBehaviour
{
    public static TimerTopos Instance { get; private set; }

    public event EventHandler OnImageFillAmount;

    [SerializeField] private Image timerFillImage;
    [SerializeField] private float duration = 60;
    private float currentTime;

    [SerializeField] private CanvasGroup canvasGroup;

    private bool isRunning;

    void Start()
    {
        canvasGroup.alpha = 0f;
        BeginTopos.Instance.OnBeginTopos += BeginTopos_OnBeginTopos;
    }

    private void BeginTopos_OnBeginTopos(object sender, System.EventArgs e)
    {
        ResetTimer();
        StartFillAmount();
    }

    

    private void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;
        float fill = Mathf.Clamp01(currentTime / duration);
        timerFillImage.fillAmount = fill;
        Debug.Log(currentTime);

        if (currentTime <= 0f)
        {
            StopTimer();
            currentTime = 0f;
            OnImageFillAmount?.Invoke(this, EventArgs.Empty);
        }
    }

    private void StartFillAmount()
    {
        isRunning = true;
        canvasGroup.alpha = 1;
    }

    private void ResetTimer()
    {
        currentTime = duration;
        timerFillImage.fillAmount = 1f;
    }

    private void StopTimer()
    {
        isRunning = false;
    }
}
