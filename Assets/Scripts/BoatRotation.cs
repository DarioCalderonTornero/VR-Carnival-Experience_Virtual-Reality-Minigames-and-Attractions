using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class BoatRotation : MonoBehaviour
{
    public float maxRotationAngle = 60f; // Ángulo máximo de rotación (en grados)
    public float tiempoTotal = 10f;      // Tiempo total de la animación (en segundos)

    private void Start()
    {
        // Iniciamos el movimiento con rotación
        RotarBarco();
    }

    void RotarBarco()
    {
        // Guardamos la rotación inicial en X y Y
        Vector3 rotacionInicial = transform.rotation.eulerAngles;

        // Comenzamos la rotación con un pequeño ángulo y velocidad lenta
        DG.Tweening.Sequence secuencia = DG.Tweening.DOTween.Sequence();  // Especifica el espacio de nombres completo

        // Rotación hacia el máximo ángulo en Z, sin cambiar X ni Y
        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, maxRotationAngle), tiempoTotal / 2, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad) // Empieza lento y acelera hacia el medio, luego desacelera
            .OnUpdate(() =>
            {
                // Ajustamos la velocidad a medida que avanza el progreso de la animación
                float progresion = secuencia.Elapsed() / (tiempoTotal / 2);
                float velocidadAjustada = Mathf.Lerp(0, 1, progresion); // Aumenta la velocidad

                // A medida que el tiempo pasa, también aumentamos el ángulo de rotación
                maxRotationAngle = Mathf.Lerp(0, maxRotationAngle, velocidadAjustada);
            })
        );

        // Vuelve a su posición inicial, manteniendo X y Y constantes
        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, -maxRotationAngle), tiempoTotal / 2, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad) // Vuelve a su posición inicial, con el mismo comportamiento suave
        );

        // Bucle infinito en vaivén
        secuencia.SetLoops(-1, LoopType.Yoyo); // Hace que el movimiento sea cíclico (va y vuelve)
    }
}
// DG.Tweening.Sequence secuencia = DG.Tweening.DOTween.Sequence();  // Especifica el espacio de nombres completo
