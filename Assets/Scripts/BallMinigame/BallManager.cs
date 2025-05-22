using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine;
using System;

public class BallManager : MonoBehaviour, IMinigame
{
    public static BallManager Instance { get; private set; }

    public event EventHandler OnGameStarted;

    [SerializeField] private BallPrefab spawner;
    [SerializeField] private ContinuousMoveProvider moveProvider;

    private bool gameActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
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
        DuckSpawnPrefab.Instance.SpawnDuck();
        OnGameStarted?.Invoke(this, EventArgs.Empty);
        Debug.Log("Minijuego de pelotas iniciado");
    }

    public void EndGame()
    {
        gameActive = false;

        Invoke(nameof(PlayerMove), 5f);

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
            Destroy(ball);

        foreach (GameObject duck in GameObject.FindGameObjectsWithTag("Duck"))
            Destroy(duck);
    }

    private void PlayerMove()
    {
        moveProvider.enabled = true;
    }

    public void OnBallDestroyed()
    {
        if (gameActive)
            spawner.SpawnBall();
    }

    public bool IsGameActive() => gameActive;

}
