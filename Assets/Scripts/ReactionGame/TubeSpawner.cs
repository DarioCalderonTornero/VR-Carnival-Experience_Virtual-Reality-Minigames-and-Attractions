using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TubeManager : MonoBehaviour
{
    [Header("Tubo y Spawners")]
    public GameObject tubePrefab;
    public Transform[] spawnPoints;

    [Header("Tiempo entre tubos")]
    public float minDelay = 1f;
    public float maxDelay = 3f;

    private List<Transform> availableSpawns = new List<Transform>();
    private List<GameObject> activeTubes = new List<GameObject>();

    private bool gameActive = false;

    public void StartGame()
    {
        if (gameActive) return;

        foreach (GameObject tube in activeTubes)
        {
            if (tube != null)
                Destroy(tube);
        }
        activeTubes.Clear();

     
        availableSpawns.Clear();
        availableSpawns.AddRange(spawnPoints);
        gameActive = true;

        StartCoroutine(SpawnTubesRandomly());
    }


    public bool IsGameActive()
    {
        return gameActive;
    }

    private IEnumerator SpawnTubesRandomly()
    {
        while (availableSpawns.Count > 0)
        {
            int index = Random.Range(0, availableSpawns.Count);
            Transform spawnPoint = availableSpawns[index];

            GameObject tube = Instantiate(tubePrefab, spawnPoint.position, Quaternion.identity);
            activeTubes.Add(tube);

            availableSpawns.RemoveAt(index);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }

        gameActive = false;
        OnGameFinished();
    }

 
    private void OnGameFinished()
    {
        Debug.Log("Minijuego de tubos finalizado.");
       
    }
}
