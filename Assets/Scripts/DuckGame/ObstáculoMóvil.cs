using UnityEngine;

public class ObstáculoMóvil : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;

    private Transform objetivoActual;
    private bool enMovimiento = false;

    void Start()
    {
        objetivoActual = puntoB;
    }

    void Update()
    {
        if (!enMovimiento) return;

        transform.position = Vector3.MoveTowards(transform.position, objetivoActual.position, velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position, objetivoActual.position) < 0.1f)
        {
            objetivoActual = (objetivoActual == puntoA) ? puntoB : puntoA;
        }
    }

    public void IniciarMovimiento()
    {
        enMovimiento = true;
    }

    public void DetenerMovimiento()
    {
        enMovimiento = false;
    }
}
