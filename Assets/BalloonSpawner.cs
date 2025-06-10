using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour
{
    [Header("Prefab del globo")]
    public GameObject balloonPrefab;
    public Ballon_Score ballon_Score;
    public BallonFinalScore ballonFinalScore;

    [Header("Puntos de salida (agujeros)")]
    public Transform[] spawnPoints;

    [Header("Tiempos de aparición")]
    public float startInterval = 1.5f;
    public float minInterval = 0.1f;
    public float acceleration = 0.01f;

    [Header("Globos por tanda")]
    public int maxBalloonsPerBatch = 4;
    public int globosParaSubirCantidad = 10;

    private float currentInterval;
    private int balloonsSpawned = 0;
    private int balloonsPerBatch = 1;

    public Transform hand;

    // Nueva variable para controlar si se debe seguir generando
    private bool isSpawning = true;

    // Tiempo tras el cual se detendrá el spawn
    public float spawnDuration = 10f;

    void Start()
    {
        hand.gameObject.SetActive(false);
        currentInterval = startInterval;
        StartCoroutine(SpawnBalloonsLoop());
        StartCoroutine(StopSpawningAfterDelay(spawnDuration));
    }

    public void BeginBalloonLoop()
    {
        isSpawning = true;
        StartCoroutine(SpawnBalloonsLoop());
        StartCoroutine(StopSpawningAfterDelay(spawnDuration));
    }

    public IEnumerator SpawnBalloonsLoop()
    {
        hand.gameObject.SetActive(true);

        while (isSpawning)
        {
            yield return new WaitForSeconds(currentInterval);

            int cantidad = Mathf.Min(balloonsPerBatch, spawnPoints.Length);
            Transform[] puntosElegidos = ElegirPuntosAleatorios(cantidad);

            foreach (Transform punto in puntosElegidos)
            {
                Instantiate(balloonPrefab, punto.position, Quaternion.identity);
                balloonsSpawned++;
            }

            if (balloonsSpawned % globosParaSubirCantidad == 0 && balloonsPerBatch < maxBalloonsPerBatch)
            {
                balloonsPerBatch++;
            }

            currentInterval = Mathf.Max(minInterval, currentInterval - acceleration);
        }

        hand.gameObject.SetActive(false);
        ballon_Score.score_balloon = 0;
        ballonFinalScore.Show();
    }

    private IEnumerator StopSpawningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSpawning = false;
    }

    Transform[] ElegirPuntosAleatorios(int cantidad)
    {
        Transform[] copia = (Transform[])spawnPoints.Clone();
        System.Random rng = new System.Random();
        for (int i = copia.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (copia[i], copia[j]) = (copia[j], copia[i]);
        }

        Transform[] resultado = new Transform[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            resultado[i] = copia[i];
        }
        return resultado;
    }
}

