using TMPro;
using UnityEngine;

public class PeriodTextUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI periodText;

    private void Update()
    {
        periodText.text = PeriodManager.Instance.GetCurrentRound().ToString();
    }
}
