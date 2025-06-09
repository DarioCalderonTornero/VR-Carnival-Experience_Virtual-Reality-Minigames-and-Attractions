using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToposFinalScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultsText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText2;

    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText2;

    [SerializeField] private TextMeshProUGUI normalToposHitText;
    [SerializeField] private TextMeshProUGUI normalToposHitText2;

    [SerializeField] private TextMeshProUGUI redToposHitText;
    [SerializeField] private TextMeshProUGUI redToposHitText2;

    [SerializeField] private TextMeshProUGUI goldToposHitText;
    [SerializeField] private TextMeshProUGUI goldToposHitText2;

    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backgroundImage2;
    [SerializeField] private Image basketballImage;
    [SerializeField] private Image decorationImage;
    [SerializeField] private Image decorationIMage2;

    private void Start()
    {
        Hide();
        BasketTimer.OnBasketTimerFinish += BasketTimer_OnBasketTimerFinish;
        TimerTopos.Instance.OnTimerFinish += TimerTopos_OnTimerFinish;
    }

    private void TimerTopos_OnTimerFinish(object sender, System.EventArgs e)
    {
        StartCoroutine(Show());
    }

    private void BasketTimer_OnBasketTimerFinish(object sender, System.EventArgs e)
    {
        StartCoroutine(Show());
    }

    private void Update()
    {
        finalScoreText.text = ToposScoreManager.Instance.GetCurrentScore().ToString();
        normalToposHitText.text = ToposScoreManager.Instance.GetNormalScore().ToString();   
        redToposHitText.text = ToposScoreManager.Instance.GetRedScore().ToString();
        goldToposHitText.text = ToposScoreManager.Instance.GetGoldScore().ToString();
        bestScoreText.text = ToposScoreManager.Instance.GetBestScore().ToString();
    }

    private IEnumerator Show()
    {
        finalScoreText.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        resultsText.gameObject.SetActive(true);
        decorationImage.gameObject.SetActive(true);
        decorationIMage2.gameObject.SetActive(true);
        messageText.gameObject.SetActive(true);
        basketballImage.gameObject.SetActive(true);
        backgroundImage2.gameObject.SetActive(true);
        goldToposHitText.gameObject.SetActive(true);
        normalToposHitText.gameObject.SetActive(true);
        redToposHitText.gameObject.SetActive(true);

        finalScoreText2.gameObject.SetActive(true);
        bestScoreText2.gameObject.SetActive(true);
        normalToposHitText2.gameObject.SetActive(true);
        goldToposHitText2.gameObject.SetActive(true);
        redToposHitText2.gameObject.SetActive(true);    

        yield return new WaitForSeconds(7);
        Hide();
    }

    private void Hide()
    {
        finalScoreText.gameObject.SetActive(false);
        bestScoreText.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        resultsText.gameObject.SetActive(false);
        decorationImage.gameObject.SetActive(false);
        decorationIMage2.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        basketballImage.gameObject.SetActive(false);
        backgroundImage2.gameObject.SetActive(false);
        goldToposHitText.gameObject.SetActive(false);
        normalToposHitText.gameObject.SetActive(false);
        redToposHitText.gameObject.SetActive(false);

        finalScoreText2.gameObject.SetActive(false);
        bestScoreText2.gameObject.SetActive(false);
        normalToposHitText2.gameObject.SetActive(false);
        goldToposHitText2.gameObject.SetActive(false);
        redToposHitText2.gameObject.SetActive(false);
    }
}
