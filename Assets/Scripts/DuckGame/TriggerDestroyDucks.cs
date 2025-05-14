using UnityEngine;

public class TriggerDestroyDucks : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pato"))
        {
            Destroy(other.gameObject);
        }
    }
}
