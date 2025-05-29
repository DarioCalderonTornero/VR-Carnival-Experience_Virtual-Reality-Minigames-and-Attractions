using TMPro;
using UnityEngine;

public class BasketScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI basketScoreText;
    private int puntosCount = 0;

    void Start()
    {
        Basketball.OnCanasta += Basketball_OnCanasta;
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
