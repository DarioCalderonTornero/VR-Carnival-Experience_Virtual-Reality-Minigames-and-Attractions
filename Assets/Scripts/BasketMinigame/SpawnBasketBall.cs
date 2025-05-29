using UnityEngine;

public class SpawnBasketBall : MonoBehaviour
{
    public static SpawnBasketBall Instance {  get; private set; }

    [SerializeField] private GameObject basketBall;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBasketBalls(Transform basketballTransform)
    {
        GameObject basketball = Instantiate(basketBall, basketballTransform.position, Quaternion.identity);
    }
}
