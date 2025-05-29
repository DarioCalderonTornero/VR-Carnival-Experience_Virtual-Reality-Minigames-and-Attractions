using System;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    public static event EventHandler OnCanasta;
    [SerializeField] private Transform particleSystemTransform;
    [SerializeField] private ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Net"))
        {
            Debug.Log("DestruirBola");
            OnCanasta?.Invoke(this, EventArgs.Empty);
            ParticleSystem particle = Instantiate (ps, particleSystemTransform.transform.position, Quaternion.identity);   
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
