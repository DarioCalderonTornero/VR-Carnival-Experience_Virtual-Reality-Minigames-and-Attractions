using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] private TextMeshProUGUI oneText;

    private void Start()
    {
        Duck.OnAnyDuckDetected += Duck_OnAnyDuckDetected;
        ToposScoreManager.Instance.OnNormalTopo += ToposScoreManager_OnNormalTopo;
        ToposScoreManager.Instance.OnGoldTopo += ToposScoreManager_OnGoldTopo;
        ToposScoreManager.Instance.OnRedTopo += ToposScoreManager_OnRedTopo;
    }

    private void ToposScoreManager_OnRedTopo(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
        oneText.text = "-1";
    }

    private void ToposScoreManager_OnGoldTopo(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
        oneText.text = "+3";
    }

    private void ToposScoreManager_OnNormalTopo(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
        oneText.text = "+1";
    }

    private void Duck_OnAnyDuckDetected(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
        oneText.text = "+1";
    }

}
