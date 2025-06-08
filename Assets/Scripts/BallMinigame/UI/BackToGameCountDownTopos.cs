using TMPro;
using UnityEngine;

public class BackToGameCountDownTopos : MonoBehaviour
{
    public static BackToGameCountDownTopos Instance {  get; private set; }

    [SerializeField] private TextMeshProUGUI backToGameToposText;

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
        backToGameToposText.text = ZonaJugableTopos.Instance.CurrentTime().ToString("F1");
    }

    public void Show()
    {
        backToGameToposText.gameObject.SetActive(true);
    }

    public void Hide()
    {
        backToGameToposText.gameObject.SetActive(false);
    }
}
