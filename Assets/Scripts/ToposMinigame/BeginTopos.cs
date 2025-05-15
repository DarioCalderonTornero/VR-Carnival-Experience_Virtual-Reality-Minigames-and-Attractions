using System;
using System.Collections;
using UnityEngine;

public class BeginTopos : MonoBehaviour
{
    public static BeginTopos Instance { get; private set; }

    public event EventHandler OnBeginTopos;

    [SerializeField] private GameObject[] hammerGameObjects;
    [SerializeField] private float delayBetweenHammers = 1f;

    [SerializeField] private Camera cam; // La cámara del jugador (cabeza)
    [SerializeField] private Transform targetTransform; // El transform al que la cámara debe mirar
    [SerializeField] private Transform playerBody; // El cuerpo del jugador o XR Rig

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
            RotatePlayerToTarget(); // Rota todo el XR Rig o jugador
            StartCoroutine(ShowHammersWithDelay());
        }
    }

    private IEnumerator ShowHammersWithDelay()
    {
        yield return new WaitForSeconds(2f);
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

    private void RotatePlayerToTarget()
    {
        // Calcular la rotación deseada
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - cam.transform.position);

        // Rotar el objeto que contiene la cámara 
        StartCoroutine(RotateOverTime(targetRotation));
    }

    private IEnumerator RotateOverTime(Quaternion targetRotation)
    {
        // Mientras la rotación no sea lo suficientemente cercana, sigue rotando
        while (Quaternion.Angle(playerBody.rotation, targetRotation) > 0.1f)
        {
            // Realizar la interpolación suave para rotar lentamente
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, Time.deltaTime * 2f);
            yield return null;
        }
    }

    private void HideHammers()
    {
        foreach (var ham in hammerGameObjects)
        {
            ham.SetActive(false);
        }
    }
}
