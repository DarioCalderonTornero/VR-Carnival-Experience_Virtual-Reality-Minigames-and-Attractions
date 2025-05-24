using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance { get; private set; }

    public event EventHandler OnImageFillAmount;

    [SerializeField] private Image timerFillImage;
    [SerializeField] private float duration = 60;

    [SerializeField] private Transform salidaTransform;
    [SerializeField] private Transform playerTransform;

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
        BallManager.Instance.OnGameStarted += BallManager_OnGameStarted;
        BallManager.Instance.OnGameEnded += BallManager_OnGameEnded;
    }

    private void BallManager_OnGameEnded(object sender, EventArgs e)
    {
        StopTimer();
        ResetTimer();
    }

    private void BallManager_OnGameStarted(object sender, EventArgs e)
    {
        ResetTimer();
        StartFillAmount();
    }

    private void Update()
    {
        if (!isRunning)
            return;

        if (BeginMiniGame.Instance.started)
        {
            currentTime -= Time.deltaTime;
            float fill = Mathf.Clamp01(currentTime / duration);
            timerFillImage.fillAmount = fill;
            //Debug.Log(currentTime);

            if (currentTime <= 0f)
            {
                StopTimer();
                currentTime = 0f;
                OnImageFillAmount?.Invoke(this, EventArgs.Empty);
                //StartCoroutine(TPPlayer());
            }
        }

        else
        {
            canvasGroup.alpha = 0f;
        }
    }

    public float CurrentTime()
    {
        return currentTime;
    }

    /*
    private IEnumerator TPPlayer()
    {
        yield return new WaitForSeconds(5f);
        FadeController.Instance.FadeIn();
        yield return new WaitForSeconds(1f);
        playerTransform.position = salidaTransform.position;
        yield return new WaitForSeconds(1f);
        FadeController.Instance.FadeOut();
        BeginMiniGame.Instance.started = false;
    }
    */

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
