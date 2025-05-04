using UnityEngine;
using UnityEngine.Splines;

public class SplineSpeedController : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimate;

    [Header("Configuración de tramos")]
    [SerializeField, Range(0f, 1f)] private float tramo1Fin = 0.3f;
    [SerializeField, Range(0f, 1f)] private float tramo2Fin = 0.6f;

    [Header("Velocidades por tramo")]
    [SerializeField] private float velocidadTramo1 = 4f;
    [SerializeField] private float velocidadTramo2 = 10f;
    [SerializeField] private float velocidadTramo3 = 6f;

    private void Update()
    {
        if (splineAnimate == null || !splineAnimate.IsPlaying)
            return;

        float t = splineAnimate.NormalizedTime;

        if (t < tramo1Fin)
        {
            //splineAnimate.maxSpeed = velocidadTramo1;
        }
        else if (t < tramo2Fin)
        {
            //splineAnimate.Speed = velocidadTramo2;
        }
        else
        {
            //splineAnimate.Speed = velocidadTramo3;
        }
    }
}
