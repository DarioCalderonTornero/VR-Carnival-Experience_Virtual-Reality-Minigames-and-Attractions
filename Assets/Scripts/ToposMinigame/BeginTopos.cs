using System;
using System.Collections;
using UnityEngine;

public class BeginTopos : MonoBehaviour
{
    public static BeginTopos Instance { get; private set; }

    public event EventHandler OnBeginTopos;

    [SerializeField] private GameObject[] hammerGameObjects;
    [SerializeField] private float delayBetweenHammers = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideHammers();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ShowHammersWithDelay());
        }
    }

    private IEnumerator ShowHammersWithDelay()
    {
        foreach (var ham in hammerGameObjects)
        {
            ham.SetActive(true);

            // Activar partículas si existen
            ParticleSystem ps = ham.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }

            yield return new WaitForSeconds(delayBetweenHammers);
        }

        OnBeginTopos?.Invoke(this, EventArgs.Empty);
    }

    private void HideHammers()
    {
        foreach (var ham in hammerGameObjects)
        {
            ham.SetActive(false);
        }
    }
}
