using System;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    public static event EventHandler OnCanasta;
    public static event EventHandler OnBallDestroy;
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

            OnBallDestroy?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            if (!PeriodManager.Instance.minigameStarted)
            {
                PeriodManager.Instance.BeginBasketPeriod();
            }

            OnBallDestroy?.Invoke(this, EventArgs.Empty);
            Destroy(this.gameObject);
        }
    }
}
