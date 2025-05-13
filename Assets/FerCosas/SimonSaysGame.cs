using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysGame : MonoBehaviour
{
    [Header("Lista de botones (ordenados)")]
    public SimonButton[] buttons;

    [Header("Configuración del juego")]
    public float flashDelay = 1f;
    public float failDisplayTime = 2f;

    [Header("Detección de proximidad")]
    public float activationDistance = 2f;
    public Transform playerTransform;      // El XR Rig o la cámara del jugador
    public Transform gameCenterPoint;      // Punto central de los botones

    private List<int> sequence = new List<int>();
    private int currentStep = 0;
    private bool isPlayerTurn = false;
    private bool gameStarted = false;

    void Update()
    {
        // Detecta si el jugador se acerca lo suficiente para iniciar el juego
        if (!gameStarted && Vector3.Distance(playerTransform.position, gameCenterPoint.position) <= activationDistance)
        {
            gameStarted = true;
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
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

        foreach (int id in sequence)
        {
            buttons[id].Flash();
            yield return new WaitForSeconds(flashDelay);
        }
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

        // Todos los botones en rojo
        foreach (var button in buttons)
        {
            button.SetColor(Color.red);
        }

        Debug.Log("¡Has fallado! Reiniciando en " + failDisplayTime + " segundos...");

        yield return new WaitForSeconds(failDisplayTime);

        // Restaurar colores base
        foreach (var button in buttons)
        {
            button.SetColor(button.baseColor);
        }

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
