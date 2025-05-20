using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;      

    private int score = 0;

    private void Start()
    {
        Duck.OnAnyDuckDetected += Duck_OnAnyDuckDetected;
        Timer.Instance.OnImageFillAmount += OnGameEnd;

        scoreText.gameObject.SetActive(false);
    }

    private void Duck_OnAnyDuckDetected(object sender, System.EventArgs e)
    {
        Debug.Log(score);
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
        

        if (Timer.Instance != null)
            Timer.Instance.OnImageFillAmount -= OnGameEnd;
    }
}
