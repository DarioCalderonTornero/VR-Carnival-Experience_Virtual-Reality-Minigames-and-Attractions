using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ToposSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] holePositions;

    [Header("Topos")]
    [SerializeField] private GameObject goldenTopo;
    [SerializeField] private GameObject normalTopo;
    [SerializeField] private GameObject redTopo;

    [Header("Spawn General")]
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private int maxToposSimultaneous = 2;

    private float elapsedTime;
    private bool spawningActive = false;
    private List<Transform> occupiedHoles = new();

    private void Start()
    {
        TimerTopos.Instance.OnImageFillAmount += TimerTopos_OnImageFillAmount;
        Hammer.OnHammerTriggered += Hammer_OnHammerTriggered;
    }

    private void Hammer_OnHammerTriggered()
    {
        StartCoroutine(SpawnerLoop());
    }

    private void TimerTopos_OnImageFillAmount(object sender, System.EventArgs e)
    {
        spawningActive = false;
        StopAllCoroutines();
    }

    public void BeginSpawn()
    {
        spawningActive = true;
        elapsedTime = 0f;
    }

    private IEnumerator SpawnerLoop()
    {
        yield return new WaitUntil(() => spawningActive);

        while (spawningActive)
        {
            elapsedTime += spawnInterval;
            AdjustPhase(elapsedTime);

            if (GameObject.FindGameObjectsWithTag("Topo").Length < maxToposSimultaneous)
            {
                SpawnTopo();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void AdjustPhase(float time)
    {
        if (time < 20f)
        {
            maxToposSimultaneous = 2;
            spawnInterval = 1.5f;
        }
        else if (time < 40f)
        {
            maxToposSimultaneous = 3;
            spawnInterval = 1.2f;
        }
        else
        {
            maxToposSimultaneous = 4;
            spawnInterval = 0.9f;
        }
    }

    private void SpawnTopo()
    {
        Transform hole = GetRandomFreeHole();
        if (hole == null) return;

        GameObject prefab = ChooseRandomTopo();
        Instantiate(prefab, hole.position, Quaternion.identity);
    }

    private Transform GetRandomFreeHole()
    {
        List<Transform> available = new List<Transform>(holePositions);

        // Elimina los agujeros ocupados
        foreach (GameObject topo in GameObject.FindGameObjectsWithTag("Topo"))
        {
            available.RemoveAll(h => Vector3.Distance(h.position, topo.transform.position) < 0.1f);
        }

        if (available.Count == 0) return null;

        return available[Random.Range(0, available.Count)];
    }

    private GameObject ChooseRandomTopo()
    {
        float rand = Random.value;

        if (rand < 0.7f)
            return normalTopo;
        else if (rand < 0.9f)
            return redTopo;
        else
            return goldenTopo;
    }

}
