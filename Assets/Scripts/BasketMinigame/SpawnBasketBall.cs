using UnityEngine;

public class SpawnBasketBall : MonoBehaviour
{
    public static SpawnBasketBall Instance {  get; private set; }

    [SerializeField] private GameObject basketBall;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PeriodManager.Instance.OnGameFinish += PeriodManager_OnGameFinish;
    }

    private void PeriodManager_OnGameFinish(object sender, System.EventArgs e)
    {
        //SpawnBasketBalls(firstBasketBallTransform);
    }

    public void SpawnBasketBalls(Transform basketballTransform)
    {
        GameObject basketball = Instantiate(basketBall, basketballTransform.position, Quaternion.identity);
    }
}
