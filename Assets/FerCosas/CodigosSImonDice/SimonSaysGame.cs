using System.Collections.Generic;
using UnityEngine;

public class SimonSaysGame : MonoBehaviour
{
    [Header("Botones")]
    public SimonButton[] buttons;

    [Header("Configuración del juego")]
    public float flashDelay = 1f;
    public float failDisplayTime = 2f;

    [Header("Proximidad")]
    public float activationDistance = 2f;
    public Transform playerTransform;
    public Transform gameCenterPoint;

    private SimonSoundManager soundManager;

    private List<int> sequence = new List<int>();
    private int currentStep = 0;
    private bool isPlayerTurn = false;
    private bool gameRunning = false;
    private bool hasEnteredProximity = false;

    // Estados
    private enum GameState { Idle, StartDelay, PlayingSequence, WaitBetweenFlashes, WaitAfterFailure, PlayerTurn, NextRoundDelay }
    private GameState currentState = GameState.Idle;

    private float stateTimer = 0f;
    private int flashIndex = 0;

    void Start()
    {
        soundManager = FindFirstObjectByType<SimonSoundManager>();
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, gameCenterPoint.position);

        if (!hasEnteredProximity && distance <= activationDistance)
        {
            hasEnteredProximity = true;
            // StartGame();
        }
        else if (hasEnteredProximity && distance > activationDistance + 0.5f)
        {
            ResetGame();
        }

        HandleGameState();
    }

    public void StartGame()
    {
        gameRunning = true;
        sequence.Clear();
        AddRandomStep();
        currentState = GameState.StartDelay;
        stateTimer = 1f;
    }

    void ResetGame()
    {
        hasEnteredProximity = false;
        gameRunning = false;
        isPlayerTurn = false;
        sequence.Clear();
        currentState = GameState.Idle;

        foreach (var button in buttons)
        {
            button.SetInteractable(false);
            button.SetColor(button.baseColor);
        }

        Debug.Log("Jugador se alejó. El minijuego se ha reiniciado.");
    }

    void AddRandomStep()
    {
        int newStep = Random.Range(0, buttons.Length);
        sequence.Add(newStep);
    }

    void HandleGameState()
    {
        if (!gameRunning) return;

        switch (currentState)
        {
            case GameState.StartDelay:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    flashIndex = 0;
                    DisableAllButtons();
                    currentState = GameState.PlayingSequence;
                }
                break;

            case GameState.PlayingSequence:
                if (flashIndex < sequence.Count)
                {
                    buttons[sequence[flashIndex]].Flash();
                    stateTimer = flashDelay;
                    currentState = GameState.WaitBetweenFlashes;
                }
                else
                {
                    EnableAllButtons();
                    currentStep = 0;
                    isPlayerTurn = true;
                    currentState = GameState.PlayerTurn;
                }
                break;

            case GameState.WaitBetweenFlashes:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    flashIndex++;
                    currentState = GameState.PlayingSequence;
                }
                break;

            case GameState.WaitAfterFailure:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    foreach (var button in buttons)
                        button.SetColor(button.baseColor);

                    // StartGame();
                }
                break;

            case GameState.NextRoundDelay:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                {
                    AddRandomStep();
                    flashIndex = 0;
                    DisableAllButtons();
                    currentState = GameState.PlayingSequence;
                }
                break;

            case GameState.PlayerTurn:
                // input control goes elsewhere
                break;
        }
    }

    public void ReceivePlayerInput(int buttonID)
    {
        if (!isPlayerTurn) return;

        if (buttonID == sequence[currentStep])
        {
            currentStep++;
            if (currentStep >= sequence.Count)
            {
                isPlayerTurn = false;
                DisableAllButtons();
                stateTimer = 1f;
                currentState = GameState.NextRoundDelay;
            }
        }
        else
        {
            FailSequence();
        }
    }

    void FailSequence()
    {
        isPlayerTurn = false;
        DisableAllButtons();

        soundManager?.PlayFailSound();

        foreach (var button in buttons)
        {
            button.SetColor(Color.red);
        }

        stateTimer = failDisplayTime;
        currentState = GameState.WaitAfterFailure;

        Debug.Log("¡Has fallado! Reinicio en " + failDisplayTime + " segundos...");
    }

    void DisableAllButtons()
    {
        foreach (var button in buttons)
            button.SetInteractable(false);
    }

    void EnableAllButtons()
    {
        foreach (var button in buttons)
            button.SetInteractable(true);
    }
}
