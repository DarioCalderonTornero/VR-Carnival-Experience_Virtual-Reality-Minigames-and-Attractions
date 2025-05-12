using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance { get; private set; }
    [SerializeField] private BallPrefab spawner;
    [SerializeField] private ContinuousMoveProvider moveProvider;

    private bool gameActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartGame(); 
        Timer.Instance.OnImageFillAmount += Timer_OnImageFillAmount;
    }

    private void Timer_OnImageFillAmount(object sender, System.EventArgs e)
    {
        EndGame();
    }

    public void StartGame()
    {
        gameActive = true;
        moveProvider.enabled = false;
        spawner.SpawnBall();
    }

    public void EndGame()
    {
        gameActive = false;
        moveProvider.enabled = true;

        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }

        GameObject[] ducks = GameObject.FindGameObjectsWithTag("Duck");
        foreach (GameObject duck in ducks)
        {
            Destroy(duck);
        }
    }


    public void OnBallDestroyed()
    {
        if (gameActive)
        {
            spawner.SpawnBall();
        }
    }
}


