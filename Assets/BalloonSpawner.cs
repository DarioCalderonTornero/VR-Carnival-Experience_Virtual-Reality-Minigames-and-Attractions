using UnityEngine;
using System.Collections;

public class BalloonSpawner : MonoBehaviour
{
    [Header("Prefab del globo")]
    public GameObject balloonPrefab;

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

    void Start()
    {
        hand.gameObject.SetActive(false);
        currentInterval = startInterval;
        //StartCoroutine(SpawnBalloonsLoop());
    }

    public void BeginBalloonLoop()
    {
        StartCoroutine(SpawnBalloonsLoop());
    }

    public IEnumerator SpawnBalloonsLoop()
    {
        while (true)
        {
            hand.gameObject.SetActive(true);

            yield return new WaitForSeconds(currentInterval);

            if (spawnPoints.Length == 0 || balloonPrefab == null)
                continue;

            // Elegimos globosPerBatch puntos aleatorios sin repetir
            int cantidad = Mathf.Min(balloonsPerBatch, spawnPoints.Length);
            Transform[] puntosElegidos = ElegirPuntosAleatorios(cantidad);

            foreach (Transform punto in puntosElegidos)
            {
                Instantiate(balloonPrefab, punto.position, Quaternion.identity); // Sin rotación
                balloonsSpawned++;
            }

            // Aumentar cantidad de globos por tanda cada 10 generados
            if (balloonsSpawned % globosParaSubirCantidad == 0 && balloonsPerBatch < maxBalloonsPerBatch)
            {
                balloonsPerBatch++;
            }

            // Acelerar
            currentInterval = Mathf.Max(minInterval, currentInterval - acceleration);
        }
    }

    // Selecciona N puntos únicos aleatorios del array
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
