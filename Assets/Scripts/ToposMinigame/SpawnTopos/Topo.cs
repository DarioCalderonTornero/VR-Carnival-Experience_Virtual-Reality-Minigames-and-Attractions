using DG.Tweening;
using System;
using System.Collections;
using UnityEditor.XR;
using UnityEngine;

public class Topo : MonoBehaviour
{

    public static event EventHandler OnAnyTopoDestroyed;

    public enum TopoType { normal, gold, red}
    public TopoType topoType;

    [SerializeField] private float timeToDestroy;
    [SerializeField] private float moveAmount = 0.5f;
    [SerializeField] private float timeWithoutMove = 0.3f;

    private bool alreadyhit = false;

    private void Start()
    {
        Sequence topoSequence = DOTween.Sequence();
        topoSequence.Append(transform.DOMoveY(transform.position.y + 1f, 0.5f))
                    .AppendInterval(0.3f)
                    .Append(transform.DOMoveY(transform.position.y, 0.5f));

        StartCoroutine(DestroyTopo());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyhit)
            return;

        if (other.CompareTag("Hammer"))
        {
            alreadyhit = true;
            ToposScoreManager.Instance.ScoreRegister(this);
            OnAnyTopoDestroyed?.Invoke(this, EventArgs.Empty);
            Debug.Log("Colision detected");
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroyTopo()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }
}
