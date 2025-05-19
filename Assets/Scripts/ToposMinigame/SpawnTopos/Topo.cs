using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Topo : MonoBehaviour
{

    public static event EventHandler OnAnyTopoDestroyed;

    [SerializeField] private float timeToDestroy;
    [SerializeField] private float moveAmount = 0.5f;
    [SerializeField] private float timeWithoutMove = 0.3f;


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
        if (other.CompareTag("Hammer"))
        {
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
