using UnityEngine;

public class FailZone : MonoBehaviour
{
    public int failedCatches = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pato"))
        {
            failedCatches++;
            Destroy(other.gameObject);
            Debug.Log("¡Fallo! Total: " + failedCatches);
        }
    }
}
