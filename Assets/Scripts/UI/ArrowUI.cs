using UnityEngine;
using DG.Tweening;

public class ArrowUI : MonoBehaviour
{
    //private Material arrowMaterial;

    void Start()
    {
        //arrowMaterial = GetComponent<Renderer>().material;

        transform.DOMoveY(transform.position.y + 0.6f, 0.85f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        //transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.8f)
          //  .SetLoops(-1, LoopType.Yoyo)
            //.SetEase(Ease.InOutSine);

        //arrowMaterial.DOColor(new Color(1f, 0.6f, 0.2f), "_Color", 0.8f)
            //.SetLoops(-1, LoopType.Yoyo);
    }
}
