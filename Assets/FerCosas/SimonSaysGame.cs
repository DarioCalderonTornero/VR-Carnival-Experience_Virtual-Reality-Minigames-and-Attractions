using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysGame : MonoBehaviour
{
    [Header("Lista de botones (orden: rojo, azul, verde, amarillo...)")]
    public SimonButton[] buttons;

    [Header("Tiempo entre flashes")]
    public float flashDelay = 1f;

    private List<int> sequence = new List<int>();
    private int currentStep = 0;
    private bool isPlayerTurn = false;

    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
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
                // Éxito: siguiente ronda
                StartCoroutine(NextRound());
            }
        }
        else
        {
            // Fallo
            Debug.Log("¡Has fallado! Reiniciando...");
            sequence.Clear();
            StartCoroutine(StartGame());
        }
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
