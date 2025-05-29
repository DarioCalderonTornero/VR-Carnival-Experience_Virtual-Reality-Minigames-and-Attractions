using UnityEngine;

public class PeriodManager : MonoBehaviour
{
    public static PeriodManager Instance { get; private set; }

    [SerializeField] private int maxRondas = 5;
    [SerializeField] private int lanzamientosPorRonda = 5;
    [SerializeField] private Transform[] spawnPoints;

    private int currentRound;
    private int lanzamientosRestantes;
    public bool minigameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Basketball.OnCanasta += Basketball_OnCanasta;
    }

    public bool isMinigameStarted()
    {
        return minigameStarted; 
    }

    public void BeginBasketPeriod()
    {
        minigameStarted = true;
        currentRound = 0;
        lanzamientosRestantes = lanzamientosPorRonda -1;
        SpawnBasketBall.Instance.SpawnBasketBalls(spawnPoints[currentRound]);
    }

    private void Basketball_OnCanasta(object sender, System.EventArgs e)
    {
        if (!minigameStarted) return;

        lanzamientosRestantes--;

        if (lanzamientosRestantes > 0)
        {
            SpawnBasketBall.Instance.SpawnBasketBalls(spawnPoints[currentRound]);
        }
        else
        {
            currentRound++;

            if (currentRound >= maxRondas)
            {
                Debug.Log("Fin del juego");
                minigameStarted = false;
            }
            else
            {
                lanzamientosRestantes = lanzamientosPorRonda;
                Debug.Log("Empieza ronda " + (currentRound + 1));
                SpawnBasketBall.Instance.SpawnBasketBalls(spawnPoints[currentRound]);
            }
        }
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

}
