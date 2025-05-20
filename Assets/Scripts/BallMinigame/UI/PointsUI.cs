using UnityEngine;

public class PointsUI : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;

    private void Start()
    {
        Duck.OnAnyDuckDetected += Duck_OnAnyDuckDetected;
        Topo.OnAnyTopoDestroyed += Topo_OnAnyTopoDestroyed;
    }

    private void Duck_OnAnyDuckDetected(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
    }

    private void Topo_OnAnyTopoDestroyed(object sender, System.EventArgs e)
    {
        myAnimator.SetTrigger("TopoHit");
    }
}
