using System;
using System.Collections;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [SerializeField] private GameObject hitParticlePrefab;
    public static event EventHandler OnAnyDuckDetected;
    public static event EventHandler OnAnyDuckDestroyed;

    [SerializeField] private AudioClip destroyAudioClip;

    private bool duckAlreadyHit = false;

    private void Awake()
    {
        
        BaseballDuckManager.Instance.RegisterDuck(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (duckAlreadyHit)
            return;

        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("DuckDetected");

            if (hitParticlePrefab != null)
            {
                duckAlreadyHit = true;

                SoundManager.Instance.Play3DSound(destroyAudioClip, transform.position);

                GameObject particle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);
                ParticleSystem ps = particle.GetComponent<ParticleSystem>();
                ps.Play();
                Destroy(particle, ps.main.duration + ps.main.startLifetime.constant);
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
