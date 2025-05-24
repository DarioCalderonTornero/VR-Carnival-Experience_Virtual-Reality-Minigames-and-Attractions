using System;
using System.Collections;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public static Duck Instance { get; private set; }

    [SerializeField] private GameObject hitParticlePrefab; // Nuevo: el prefab, no el ParticleSystem
    public static event EventHandler OnAnyDuckDetected;
    public static event EventHandler OnAnyDuckDestroyed;

    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("DuckDetected");

            // Instanciar la partícula donde está el pato
            if (hitParticlePrefab != null)
            {
                GameObject particle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                ps.Play();

                Destroy(particle, ps.main.duration + ps.main.startLifetime.constant); // Se autodestruye
            }

            OnAnyDuckDetected?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DestroyDuck());
        }
    }

    private IEnumerator DestroyDuck()
    {
        yield return new WaitForSeconds(0.5f);
        OnAnyDuckDestroyed?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
}
