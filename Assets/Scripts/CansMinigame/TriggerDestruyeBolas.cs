using UnityEngine;

public class TriggerDestruyeBolas : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bola"))
        {
            Destroy(other.gameObject);
        }
    }
}
