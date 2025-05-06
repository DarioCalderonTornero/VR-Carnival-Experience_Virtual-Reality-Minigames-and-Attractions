using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    public GameObject[] npcPrefabs;         // Array de prefabs de NPCs
    public int numberOfNPCs = 10;           // Número de NPCs a crear
    public Vector3 spawnAreaSize = new Vector3(50, 0, 50);  // Tamaño del parque
    public Vector3 spawnCenter = Vector3.zero;              // Centro del parque

    void Start()
    {
        for (int i = 0; i < numberOfNPCs; i++)
        {
            Vector3 randomPos = GetRandomPointInArea();
            GameObject selectedPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
            GameObject npc = Instantiate(selectedPrefab, randomPos, Quaternion.identity);
            npc.AddComponent<NPCWander>();
        }
    }

    Vector3 GetRandomPointInArea()
    {
        float x = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float z = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
        return spawnCenter + new Vector3(x, 0, z);
    }
}
