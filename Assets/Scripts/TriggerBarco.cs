using UnityEngine;

public class TriggerBarco : MonoBehaviour
{
    public BoatRotation barco; // Referencia al barco que quieres activar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            barco.IniciarAtraccion(other.gameObject);
        }
    }
}
