using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI roundAchievedText;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backgroundImage2;
    [SerializeField] private Image basketballImage;
    [SerializeField] private Image decorationImage;
    [SerializeField] private Image decorationIMage2;

    private void Start()
    {
        Hide();
        BasketTimer.OnBasketTimerFinish += BasketTimer_OnBasketTimerFinish;
    }

    private void BasketTimer_OnBasketTimerFinish(object sender, System.EventArgs e)
    {
        StartCoroutine(Show());
    }

    private void Update()
    {
        finalScoreText.text = "Total Score: " + BasketScore.Instance.GetPuntosCount().ToString();
        roundAchievedText.text = "Round achieved: " + PeriodManager.Instance.GetCurrentRound().ToString();
        bestScoreText.text = "Best Score: " + BasketScore.Instance.GetBestScore().ToString();
    }

    private IEnumerator Show()
    {
        finalScoreText.gameObject.SetActive(true);  
        bestScoreText.gameObject.SetActive(true);
        roundAchievedText.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        resultsText.gameObject.SetActive(true);
        decorationImage.gameObject.SetActive(true);
        decorationIMage2.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
        basketballImage.gameObject.SetActive(true);
        backgroundImage2.gameObject.SetActive(true);

        yield return new WaitForSeconds(7);
        Hide();
    }

    private void Hide()
    {
        finalScoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        roundAchievedText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        resultsText.gameObject.SetActive(false);
        decorationImage.gameObject.SetActive(false);
        decorationIMage2.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        basketballImage.gameObject.SetActive(false);
        backgroundImage2.gameObject.SetActive(false);
    }
}
