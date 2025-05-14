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
    public Transform playerTransform;      // Cámara o XR Rig
    public Transform gameCenterPoint;      // Punto central del minijuego

    private List<int> sequence = new List<int>();
    private int currentStep = 0;
    private bool isPlayerTurn = false;
    private bool gameStarted = false;

    void Update()
    {
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

        // Desactivar interacción de botones
        foreach (var button in buttons)
            button.SetInteractable(false);

        foreach (int id in sequence)
        {
            buttons[id].Flash();
            yield return new WaitForSeconds(flashDelay);
        }

        // Reactivar botones
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

        // Desactiva botones y los pone en rojo
        foreach (var button in buttons)
        {
            button.SetInteractable(false);
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
