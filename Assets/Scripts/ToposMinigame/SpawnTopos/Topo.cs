using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Topo : MonoBehaviour
{

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


    private IEnumerator DestroyTopo()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(this.gameObject);
    }
}
