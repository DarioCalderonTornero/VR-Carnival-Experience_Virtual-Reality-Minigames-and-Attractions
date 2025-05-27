using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using System;

public class BallManager : MonoBehaviour, IMinigame
{
    public static BallManager Instance { get; private set; }

    public event EventHandler OnGameStarted;
    public event EventHandler OnGameEnded;

    [SerializeField] private BallPrefab spawner;
    [SerializeField] private Transform triggerZoneTransform;
    [SerializeField] private Transform playerTransform; 

    public bool gameActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Timer.Instance.OnImageFillAmount += Timer_OnImageFillAmount;
        triggerZoneTransform.gameObject.SetActive(false);
    }

    private void Timer_OnImageFillAmount(object sender, EventArgs e)
    {
        EndGame();
    }

    public void StartGame()
    {
        // ACTIVA primero la lógica del juego aunque la parte visual comience después
        gameActive = true;
        triggerZoneTransform.gameObject.SetActive(true);

        // Esto permite que si el jugador se sale durante la cuenta atrás, se detecte correctamente
        Collider triggerCollider = triggerZoneTransform.GetComponent<Collider>();
        if (triggerCollider != null && triggerCollider.bounds.Contains(playerTransform.position))
        {
            BeginMiniGame beginScript = triggerZoneTransform.GetComponent<BeginMiniGame>();
            if (beginScript != null)
            {
                beginScript.ForcePlayerInside();
            }
        }

        // Luego lanzas la cuenta atrás visual
        StartCoroutine(CountDown.Instance.Countdown(() =>
        {
            BeginLogic();
        }));
    }

    private void BeginLogic()
    {
        spawner.SpawnBall();
        DuckSpawnPrefab.Instance.SpawnDuck();
        OnGameStarted?.Invoke(this, EventArgs.Empty);
        Debug.Log("Minijuego de pelotas iniciado");
    }


    public void EndGame()
    {
        OnGameEnded?.Invoke(this, EventArgs.Empty);
        BeginMiniGame.Instance.started = false;
        BaseballDuckManager.Instance.ResetDuckList();
        gameActive = false;
        triggerZoneTransform.gameObject.SetActive(false);

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
            Destroy(ball);

        foreach (GameObject duck in GameObject.FindGameObjectsWithTag("Duck"))
            Destroy(duck);
    }

    public void OnBallDestroyed()
    {
        if (gameActive)
            spawner.SpawnBall();
    }

    public bool IsGameActive() => gameActive;
}
