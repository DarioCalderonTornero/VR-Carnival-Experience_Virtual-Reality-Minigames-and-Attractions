using System;
using UnityEngine;
using UnityEngine.UI;

public class TimerTopos : MonoBehaviour
{
    public static TimerTopos Instance { get; private set; }

    public event EventHandler OnTimerFinish;
    public event EventHandler OnTimerStart; 

    [SerializeField] private Image timerFillImage;
    [SerializeField] private float duration = 60;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform salidaTransform;

    private float currentTime;

    [SerializeField] private CanvasGroup canvasGroup;

    private bool isRunning;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        canvasGroup.alpha = 0f;
        //Hammer.OnHammerTriggered += Hammer_OnHammerTriggered;
        BeginMinigameTopos.Instance.OnBeginMinigameTopos += BeginMinigameTopos_OnBeginMinigameTopos;
        //CountDown.Instance.OnCountDownFinish += CountDownTopos_OnCountDownFinish;
    }

    private void CountDownTopos_OnCountDownFinish(object sender, EventArgs e)
    {
        Debug.Log("CountDownFinish");
        //ResetTimer();
        //StartFillAmount();
    }

    private void BeginMinigameTopos_OnBeginMinigameTopos(object sender, EventArgs e)
    {
        StartFillAmount();
        ResetTimer();
    }

    /*
    private void Hammer_OnHammerTriggered()
    {
        ResetTimer();
        StartFillAmount();
    }
    */


    private void Update()
    {
        if (!isRunning)
            return;
        Debug.Log(currentTime);

        currentTime -= Time.deltaTime;
        float fill = Mathf.Clamp01(currentTime / duration);
        timerFillImage.fillAmount = fill;
        //Debug.Log(currentTime);

        if (currentTime <= 0f)
        {
            Debug.Log("TimerFinish");
            FinishMinigame();
            //BeginMinigameTopos.Instance.playerMove.enabled = true;
        }
    }

    public void FinishMinigame()
    {
        StopTimer();
        currentTime = 0f;
        FadeController.Instance.FadeIn();
        Invoke(nameof(TPPlayer), 1.5f);
        Invoke(nameof(FadeOut), 3f);
        OnTimerFinish?.Invoke(this, EventArgs.Empty);
        canvasGroup.alpha = 0f;
        BeginMinigameTopos.Instance.minigameStarted = false;
        BackToGameCountDownTopos.Instance.Hide();
    }

    private void TPPlayer()
    {
        playerTransform.position = salidaTransform.position;  
    }


    private void FadeOut()
    {
        FadeController.Instance.FadeOut();
    }

    private void StartFillAmount()
    {
        Debug.Log("StartTimer");
        isRunning = true;
        canvasGroup.alpha = 1;
        OnTimerStart?.Invoke(this, EventArgs.Empty);
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
