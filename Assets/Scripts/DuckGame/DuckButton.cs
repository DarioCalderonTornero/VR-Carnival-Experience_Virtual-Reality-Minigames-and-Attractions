using UnityEngine;

public class DuckButton : MonoBehaviour
{
    public DuckManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) 
        {
            manager.Disparar();
            Debug.Log("Funciona");
        }
    }
}
