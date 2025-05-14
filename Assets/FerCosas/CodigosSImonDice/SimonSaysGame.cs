using System.Collections;
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
            StartCoroutine(StartGame());
        }
        else if (hasEnteredProximity && distance > activationDistance + 0.5f) // un poco más de margen
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        StopAllCoroutines();
        hasEnteredProximity = false;
        gameRunning = false;
        isPlayerTurn = false;
        sequence.Clear();

        foreach (var button in buttons)
        {
            button.SetInteractable(false);
            button.SetColor(button.baseColor);
        }

        Debug.Log("Jugador se alejó. El minijuego se ha reiniciado.");
    }

    IEnumerator StartGame()
    {
        gameRunning = true;
        sequence.Clear();
        yield return new WaitForSeconds(1f);
        AddRandomStep();
        yield return PlaySequence();
        isPlayerTurn = true;
    }

    void AddRandomStep()
    {
        int newStep = Random.Range(0, buttons.Length);
        sequence.Add(newStep);
    }

    IEnumerator PlaySequence()
    {
        isPlayerTurn = false;
        currentStep = 0;

        foreach (var button in buttons)
            button.SetInteractable(false);

        foreach (int id in sequence)
        {
            buttons[id].Flash();
            yield return new WaitForSeconds(flashDelay);
        }

        foreach (var button in buttons)
            button.SetInteractable(true);
    }

    public void ReceivePlayerInput(int buttonID)
    {
        if (!isPlayerTurn)
            return;

        if (buttonID == sequence[currentStep])
        {
            currentStep++;
            if (currentStep >= sequence.Count)
            {
                StartCoroutine(NextRound());
            }
        }
        else
        {
            StartCoroutine(HandleFailure());
        }
    }

    IEnumerator HandleFailure()
    {
        isPlayerTurn = false;

        soundManager?.PlayFailSound();

        foreach (var button in buttons)
        {
            button.SetInteractable(false);
            button.SetColor(Color.red);
        }

        Debug.Log("¡Has fallado! Reinicio en " + failDisplayTime + " segundos...");
        yield return new WaitForSeconds(failDisplayTime);

        foreach (var button in buttons)
            button.SetColor(button.baseColor);

        StartCoroutine(StartGame());
    }

    IEnumerator NextRound()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);
        AddRandomStep();
        yield return PlaySequence();
        isPlayerTurn = true;
    }
}
