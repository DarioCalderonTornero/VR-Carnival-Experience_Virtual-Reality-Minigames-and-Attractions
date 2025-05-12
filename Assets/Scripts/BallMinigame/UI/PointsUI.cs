using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;

    private void Start()
    {
        Duck.Instance.OnBallDetected += Duck_OnBallDetected;
    }

    private void Duck_OnBallDetected(object sender, System.EventArgs e)
    {
        Debug.Log("Event completed");
        myAnimator.SetBool("DuckHit", true);
    }
}
