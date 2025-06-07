using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseballFinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backgroundImage2;
    [SerializeField] private Image decorationImage;
    [SerializeField] private Image decorationIMage2;
    [SerializeField] private Image baseballImage;

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
        finalScoreText.text = "Ducks hunted: " + ScoreManager.Instance.GetBaseBallScore().ToString() + " /8 ";
    }

    private IEnumerator Show()
    {
        finalScoreText.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        resultsText.gameObject.SetActive(true);
        decorationImage.gameObject.SetActive(true);
        decorationIMage2.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
        backgroundImage2.gameObject.SetActive(true);
        baseballImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(7);
        Hide();
    }

    private void Hide()
    {
        finalScoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        resultsText.gameObject.SetActive(false);
        decorationImage.gameObject.SetActive(false);
        decorationIMage2.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        backgroundImage2.gameObject.SetActive(false);
        baseballImage.gameObject.SetActive(false);
    }
}
