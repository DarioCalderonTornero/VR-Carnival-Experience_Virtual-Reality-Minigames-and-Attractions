using UnityEngine;

public class TriggerMinijuegoInicio : MonoBehaviour
{
    public FootbalMinigameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.EmpezarMinijuego();
        }
    }
}
