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
            OnCanasta?.Invoke(this, EventArgs.Empty);

            if (!PeriodManager.Instance.minigameStarted)
            {
                PeriodManager.Instance.BeginBasketPeriod();
            }

            ParticleSystem particle = Instantiate (ps, particleSystemTransform.transform.position, Quaternion.identity);   

            Destroy(this.gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
