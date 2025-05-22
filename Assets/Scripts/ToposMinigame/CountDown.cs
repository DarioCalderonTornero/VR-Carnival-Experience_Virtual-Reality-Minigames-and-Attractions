using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public static CountDown Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI countdownText;
    private bool isCountingDown = false;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Countdown(Action onFinishCallback)
    {
        if (isCountingDown) yield break;
        isCountingDown = true;

        countdownText.gameObject.SetActive(true);
        int time = 3;

        while (time >= 0)
        {
            countdownText.text = time == 0 ? "¡YA!" : time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownText.gameObject.SetActive(false);
        isCountingDown = false;

        onFinishCallback?.Invoke();
    }
}
