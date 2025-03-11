using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class BoatRotation : MonoBehaviour
{
    public float maxRotationAngle = 60f; 
    public float tiempoSubida = 2f;      

    private void Start()
    {
        RotarBarco();
    }

    void RotarBarco()
    {
        Vector3 rotacionInicial = transform.rotation.eulerAngles;

        DG.Tweening.Sequence secuencia = DG.Tweening.DOTween.Sequence();

        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, maxRotationAngle), tiempoSubida, RotateMode.FastBeyond360)
            .SetEase(Ease.InQuad)  
        );

        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, -maxRotationAngle), tiempoSubida, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad)  
            .SetLoops(-1, LoopType.Yoyo) 
        );

        
        secuencia.SetLoops(-1, LoopType.Restart); 
    }
}
