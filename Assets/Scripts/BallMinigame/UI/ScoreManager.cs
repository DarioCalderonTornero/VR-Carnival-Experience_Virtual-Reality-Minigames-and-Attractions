using System.Collections;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;      

    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Duck.OnAnyDuckDetected += Duck_OnAnyDuckDetected;
        Timer.Instance.OnImageFillAmount += OnGameEnd;
        BaseballDuckManager.Instance.OnAllDucksDestroyed += BaseballDuckManager_OnAllDucksDestroyed;

        scoreText.gameObject.SetActive(false);
    }

    private void BaseballDuckManager_OnAllDucksDestroyed(object sender, System.EventArgs e)
    {
        UpdateBestScore();
        Invoke(nameof(ResetScore), 7f);
    }

    private void OnGameEnd(object sender, System.EventArgs e)
    {
        UpdateBestScore();
        Invoke(nameof(ResetScore), 7f);
    }

    private void Duck_OnAnyDuckDetected(object sender, System.EventArgs e)
    {
        Debug.Log(score);
        score++;
    }

    private void ResetScore()
    {
        score = 0;
    }

    public int GetBaseBallScore()
    {
        return score;
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestBaseballScore", 0);
    }

    public void UpdateBestScore()
    {
        if (score > GetBestScore())
        {
            PlayerPrefs.SetInt("BestBaseballScore", score);
            PlayerPrefs.Save();
        }
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
