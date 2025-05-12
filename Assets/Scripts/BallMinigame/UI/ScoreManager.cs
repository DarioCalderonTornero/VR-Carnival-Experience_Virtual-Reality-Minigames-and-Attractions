using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;      // El texto que muestra los puntos

    private int score = 0;

    private void Start()
    {
        Duck.Instance.OnBallDetected += OnDuckHit;
        Timer.Instance.OnImageFillAmount += OnGameEnd;

        scoreText.gameObject.SetActive(false);
    }

    private void OnDuckHit(object sender, System.EventArgs e)
    {
        score++;
    }

    private void OnGameEnd(object sender, System.EventArgs e)
    {
        ShowFinalScore();
    }

    private void ShowFinalScore()
    {
        scoreText.text = "Puntos: " + score.ToString();
        scoreText.gameObject.SetActive(true);

        StartCoroutine(HidePanelAfterDelay());
    }

    private IEnumerator HidePanelAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        scoreText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Duck.Instance != null)
            Duck.Instance.OnBallDetected -= OnDuckHit;

        if (Timer.Instance != null)
            Timer.Instance.OnImageFillAmount -= OnGameEnd;
    }
}
