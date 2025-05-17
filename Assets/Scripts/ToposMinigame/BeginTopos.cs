using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class BeginTopos : MonoBehaviour
{
    public static BeginTopos Instance { get; private set; }

    public event EventHandler OnPlayerChooseHammer;

    [SerializeField] private GameObject[] hammerGameObjects;
    [SerializeField] private float delayBetweenHammers = 1f;

    [SerializeField] private Camera cam; 
    [SerializeField] private Transform targetTransform; 
    [SerializeField] private Transform playerBody;

    [SerializeField] private ContinuousMoveProvider playerMove;

    [SerializeField] private TextMeshProUGUI pickUpHammerText;

    private bool lookHammersOnce = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HideHammers();
        pickUpHammerText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!lookHammersOnce)
            {
                RotatePlayerToTarget();

            }
            StartCoroutine(ShowHammersWithDelay());
            playerMove.enabled = false; 
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

        playerMove.enabled = true;
        StartCoroutine(Show_HideHammer());
    }

    private IEnumerator Show_HideHammer()
    {
        pickUpHammerText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        pickUpHammerText.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void RotatePlayerToTarget()
    {
        // Calcular la rotación deseada
        Quaternion targetRotation = Quaternion.LookRotation(targetTransform.position - cam.transform.position);

        // Rotar el objeto que contiene la cámara 
        StartCoroutine(RotateOverTime(targetRotation));

        lookHammersOnce = true;
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
