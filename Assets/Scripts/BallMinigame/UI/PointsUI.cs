using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;

    private void Start()
    {
        Duck.Instance.OnBallDetected += Duck_OnBallDetected;
        Topo.OnAnyTopoDestroyed += Topo_OnAnyTopoDestroyed;
    }

    private void Topo_OnAnyTopoDestroyed(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
    }

    private void Duck_OnBallDetected(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
    }


}
