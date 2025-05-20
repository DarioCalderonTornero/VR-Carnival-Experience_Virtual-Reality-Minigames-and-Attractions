using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public static event Action OnHammerTriggered;
    public bool hasStarted;

    [SerializeField] private ParticleSystem hitParticlePrefab;
    [SerializeField] private Transform particleTransform;

    public void HanldeGrab()
    {
        if (hasStarted)
            return;

        hasStarted = true;

        OnHammerTriggered?.Invoke();

        //ToposManager.Instance.StartToposMinigame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Topo"))
        {
            if (hitParticlePrefab != null)
            {
                ParticleSystem particle = Instantiate(hitParticlePrefab, particleTransform.position, Quaternion.identity);
                particle.Play();
                //Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.constant); 
            }
        }
    }
}
