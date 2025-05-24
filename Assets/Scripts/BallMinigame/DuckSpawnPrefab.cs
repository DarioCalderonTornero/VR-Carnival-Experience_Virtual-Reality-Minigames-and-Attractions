using UnityEngine;

public class DuckSpawnPrefab : MonoBehaviour
{
    public static DuckSpawnPrefab Instance {  get; private set; }

    [SerializeField] private GameObject duckGameObject;
    [SerializeField] private Transform[] duckSpawnPoint;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnDuck()
    {
        foreach (Transform t in duckSpawnPoint)
        {
            GameObject duckGO = Instantiate(duckGameObject, t.position, t.rotation);

            Duck duck = duckGO.GetComponent<Duck>(); 

            if (duck != null)
            {
                BaseballDuckManager.Instance.RegisterDuck(duck); 
            }
        }
    }

}
