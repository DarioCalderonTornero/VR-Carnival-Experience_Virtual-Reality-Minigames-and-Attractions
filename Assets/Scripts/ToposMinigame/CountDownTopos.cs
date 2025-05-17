using System.Collections;
using TMPro;
using UnityEngine;

public class CountDownTopos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    private void Start()
    {
        countdownText.gameObject.SetActive(false);
        Hammer.OnHammerTriggered += Hammer_OnHammerTriggered;
    }

    private void Hammer_OnHammerTriggered()
    {
        countdownText.gameObject.SetActive(true);
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int time = 3;

        while (time >= 0)
        {
            countdownText.text = time == 0 ? "¡YA!" : time.ToString();
            yield return new WaitForSeconds(1f);
            time--;
        }

        countdownText.gameObject.SetActive(false);
    }
}
