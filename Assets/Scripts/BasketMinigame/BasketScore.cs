using TMPro;
using UnityEngine;

public class BasketScore : MonoBehaviour
{
    public static BasketScore Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI basketScoreText;
    private int puntosCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Basketball.OnCanasta += Basketball_OnCanasta;
        PeriodManager.Instance.OnGameFinish += PeriodManager_OnGameFinish;
    }

    private void PeriodManager_OnGameFinish(object sender, System.EventArgs e)
    {
        UpdateBestScore();
        Invoke(nameof(ResetPuntosCount), 7f);
    }

    private void ResetPuntosCount()
    {
        puntosCount = 0;
    }

    private void Update()
    {
        basketScoreText.text = puntosCount.ToString();
    }

    private void Basketball_OnCanasta(object sender, System.EventArgs e)
    {
        puntosCount++;
    }

    public int GetPuntosCount()
    {
        return puntosCount;
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestBasketScore", 0);
    }

    public void UpdateBestScore()
    {
        if (puntosCount > GetBestScore())
        {
            PlayerPrefs.SetInt("BestBasketScore", puntosCount);
            PlayerPrefs.Save();
        }
    }
}
