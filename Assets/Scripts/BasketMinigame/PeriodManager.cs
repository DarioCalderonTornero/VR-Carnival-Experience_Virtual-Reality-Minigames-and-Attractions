using System;
using UnityEngine;

public class PeriodManager : MonoBehaviour
{
    public static PeriodManager Instance { get; private set; }

    public event EventHandler OnGameFinish;

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
        Basketball.OnBallDestroy += Basketball_OnCanasta;
        BasketTimer.OnBasketTimerFinish += BasketTimer_OnBasketTimerFinish;
    }

    private void BasketTimer_OnBasketTimerFinish(object sender, System.EventArgs e)
    {
        Debug.Log("Time Finish");
        EndGame();
    }

    public bool isMinigameStarted()
    {
        return minigameStarted; 
    }

    public void BeginBasketPeriod()
    {
        minigameStarted = true;
        currentRound = 0;
        lanzamientosRestantes = lanzamientosPorRonda;
        //SpawnBasketBall.Instance.SpawnBasketBalls(spawnPoints[currentRound]);
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
                EndGame();
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

    public void EndGame()
    {
        //Reinicio nivel
        Debug.Log("Game Ended");
        OnGameFinish?.Invoke(this, EventArgs.Empty);
        Invoke(nameof(ResetCurrentRound),7f);
        lanzamientosRestantes = lanzamientosPorRonda;

        foreach(GameObject ball in GameObject.FindGameObjectsWithTag("BasketBall"))
        {
            Destroy(ball);
        }
        Invoke(nameof(SpawnFistBasketBall), 7f);

        BasketManager.Instance.ResetMinigame();
    }

    private void ResetCurrentRound()
    {
        currentRound = 0;
    }

    private void SpawnFistBasketBall()
    {
        SpawnBasketBall.Instance.SpawnBasketBalls(spawnPoints[currentRound]);
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

}
