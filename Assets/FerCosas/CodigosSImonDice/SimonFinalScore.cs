using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimonFinalScore : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Decoración")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backgroundImage2;
    [SerializeField] private Image decorationImage;
    [SerializeField] private Image decorationImage2;

    [Header("Botón de reinicio")]
    [SerializeField] private Button restartButton;

    private void Start()
    {
        Hide();

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }

    private void Update()
    {
        if (SimonScore.Instance != null)
        {
            finalScoreText.text = "Rondas Completadas: " + SimonScore.Instance.GetScore();
            bestScoreText.text = "Mejor Puntuación: " + SimonScore.Instance.GetBestScore();
        }
    }

    public void Show()
    {
        finalScoreText.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        resultsText.gameObject.SetActive(true);
        decorationImage.gameObject.SetActive(true);
        decorationImage2.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
        backgroundImage2.gameObject.SetActive(true);
    }

    private void Hide()
    {
        finalScoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        resultsText.gameObject.SetActive(false);
        decorationImage.gameObject.SetActive(false);
        decorationImage2.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        backgroundImage2.gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        Hide();
        SimonSaysGame gameManager = FindFirstObjectByType<SimonSaysGame>();
        if (gameManager != null)
        {
            gameManager.StartGame();
        }
    }
}
