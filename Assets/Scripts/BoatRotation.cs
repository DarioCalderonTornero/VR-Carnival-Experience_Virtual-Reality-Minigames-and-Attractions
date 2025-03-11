using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class BoatRotation : MonoBehaviour
{
    public float maxRotationAngle = 60f; // �ngulo m�ximo de rotaci�n (en grados)
    public float tiempoTotal = 10f;      // Tiempo total de la animaci�n (en segundos)

    private void Start()
    {
        // Iniciamos el movimiento con rotaci�n
        RotarBarco();
    }

    void RotarBarco()
    {
        // Guardamos la rotaci�n inicial en X y Y
        Vector3 rotacionInicial = transform.rotation.eulerAngles;

        // Comenzamos la rotaci�n con un peque�o �ngulo y velocidad lenta
        DG.Tweening.Sequence secuencia = DG.Tweening.DOTween.Sequence();  // Especifica el espacio de nombres completo

        // Rotaci�n hacia el m�ximo �ngulo en Z, sin cambiar X ni Y
        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, maxRotationAngle), tiempoTotal / 2, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad) // Empieza lento y acelera hacia el medio, luego desacelera
            .OnUpdate(() =>
            {
                // Ajustamos la velocidad a medida que avanza el progreso de la animaci�n
                float progresion = secuencia.Elapsed() / (tiempoTotal / 2);
                float velocidadAjustada = Mathf.Lerp(0, 1, progresion); // Aumenta la velocidad

                // A medida que el tiempo pasa, tambi�n aumentamos el �ngulo de rotaci�n
                maxRotationAngle = Mathf.Lerp(0, maxRotationAngle, velocidadAjustada);
            })
        );

        // Vuelve a su posici�n inicial, manteniendo X y Y constantes
        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, -maxRotationAngle), tiempoTotal / 2, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad) // Vuelve a su posici�n inicial, con el mismo comportamiento suave
        );

        // Bucle infinito en vaiv�n
        secuencia.SetLoops(-1, LoopType.Yoyo); // Hace que el movimiento sea c�clico (va y vuelve)
    }
}
// DG.Tweening.Sequence secuencia = DG.Tweening.DOTween.Sequence();  // Especifica el espacio de nombres completo
