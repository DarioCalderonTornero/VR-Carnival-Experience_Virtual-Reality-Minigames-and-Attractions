using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    public event EventHandler OnImageFillAmount;

    [SerializeField] private Image timerFillImage;
    [SerializeField] private float duration = 60;
    private float currentTime;

    [SerializeField] private CanvasGroup canvasGroup;

    private bool isRunning;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        BeginMiniGame.Instance.OnBeginBaseBall += BeginMiniGame_OnBeginBaseBall;
    }

    private void BeginMiniGame_OnBeginBaseBall(object sender, System.EventArgs e)
    {
        Debug.Log("Beginnnnn");
        ResetTimer();
        StartFillAmount();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;
        float fill = Mathf.Clamp01(currentTime/duration);
        timerFillImage.fillAmount = fill;
        Debug.Log(currentTime);

        if (currentTime <= 0f)
        {
            StopTimer();
            currentTime = 0f;
            OnImageFillAmount?.Invoke(this,EventArgs.Empty);
        }
    }

    private void StartFillAmount()
    {
        Debug.Log("nqu9c");
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
