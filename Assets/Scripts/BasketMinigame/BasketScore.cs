using TMPro;
using UnityEngine;

public class BasketScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI basketScoreText;
    private int puntosCount = 0;

    void Start()
    {
        Basketball.OnCanasta += Basketball_OnCanasta;
        PeriodManager.Instance.OnGameFinish += PeriodManager_OnGameFinish;
    }

    private void PeriodManager_OnGameFinish(object sender, System.EventArgs e)
    {
        Invoke(nameof(ResetPuntosCount), 3f);
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
}
