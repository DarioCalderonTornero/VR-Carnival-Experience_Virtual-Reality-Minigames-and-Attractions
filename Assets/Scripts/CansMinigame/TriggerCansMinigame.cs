using UnityEngine;

public class TriggerCansMinigame : MonoBehaviour
{
    public CansManager manager;
    public string tagJugador = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            manager.EmpezarMinijuego();
        }
    }
}
