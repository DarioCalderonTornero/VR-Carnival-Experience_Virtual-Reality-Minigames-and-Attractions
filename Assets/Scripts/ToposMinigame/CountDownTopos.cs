using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountDownTopos : MonoBehaviour
{
    public static CountDownTopos Instance { get; private set; }

    public event EventHandler OnCountDownFinish;

    [SerializeField] private TextMeshProUGUI countdownText;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        countdownText.gameObject.SetActive(false);
        //Hammer.OnHammerTriggered += Hammer_OnHammerTriggered;
        BeginMinigameTopos.Instance.OnBeginMinigameTopos += BeginMinigameTopos_OnBeginMinigameTopos;
    }

    private void BeginMinigameTopos_OnBeginMinigameTopos(object sender, System.EventArgs e)
    {
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }

    /*
    private void Hammer_OnHammerTriggered()
    {
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }
    */

    private IEnumerator CountDown()
    {
        int time = 3;

        while (time >= 0)
        {
            countdownText.text = time == 0 ? "¡YA!" : time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }
        Debug.Log("Timer Finish");
        OnCountDownFinish?.Invoke(this, EventArgs.Empty);
        countdownText.gameObject.SetActive(false);
    }
}
