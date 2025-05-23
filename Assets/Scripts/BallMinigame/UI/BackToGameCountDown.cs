using TMPro;
using UnityEngine;

public class BackToGameCountDown : MonoBehaviour
{
    public static BackToGameCountDown Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI backToGameCountDownText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        backToGameCountDownText.text = BeginMiniGame.Instance.CurrentTime().ToString("F2");
    }

    public void Show()
    {
        backToGameCountDownText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        backToGameCountDownText.gameObject.SetActive(false);
    }
}
