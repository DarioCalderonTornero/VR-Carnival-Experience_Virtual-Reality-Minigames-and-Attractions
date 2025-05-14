using UnityEngine;

public class TriggerInicio : MonoBehaviour
{
    [SerializeField] private DuckManager manager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            manager.EmpezarMinijuego();
        }
    }
}
