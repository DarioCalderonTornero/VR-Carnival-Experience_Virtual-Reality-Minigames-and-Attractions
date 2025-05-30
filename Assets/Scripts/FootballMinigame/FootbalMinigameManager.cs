using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootbalMinigameManager : MonoBehaviour
{
    [Header("Pelota")]
    public GameObject pelotaPrefab;
    public Transform posicionInstanciaPelota;

    [Header("Jugador")]
    public Transform jugador;
    public Transform zonaSalidaJugador;

    [Header("Configuración de rondas")]
    public int totalRondas = 5;

    private GameObject pelotaActual;
    private bool minijuegoActivo = false;
    private int puntos = 0;
    private int rondasActuales = 0;

    [SerializeField] private GoalKeeper portero;

    public void EmpezarMinijuego()
    {
        if (minijuegoActivo) return;

        puntos = 0;
        rondasActuales = 0;
        minijuegoActivo = true;

        portero.IniciarMovimiento();

        StartCoroutine(InstanciarPelota());
    }

    IEnumerator InstanciarPelota()
    {
        if (pelotaActual != null)
            Destroy(pelotaActual, 2f);

        yield return new WaitForSeconds(2f);

        pelotaActual = Instantiate(pelotaPrefab, posicionInstanciaPelota.position, Quaternion.identity);
        pelotaActual.GetComponent<GoslBall>().AsignarManager(this);
    }

    public void PelotaTocadaNoMesa()
    {
        if (!minijuegoActivo) return;

        rondasActuales++;
        VerificarFinMinijuego();
        if (minijuegoActivo)
            StartCoroutine(InstanciarPelota());
    }

    public void GolMarcado()
    {
        if (!minijuegoActivo) return;

        puntos++;
        rondasActuales++;
        VerificarFinMinijuego();
        if (minijuegoActivo)
            StartCoroutine(InstanciarPelota());
    }

    private void VerificarFinMinijuego()
    {
        if (rondasActuales >= totalRondas)
        {
            minijuegoActivo = false;

            if (pelotaActual != null)
                Destroy(pelotaActual, 2f);

            TerminarMinijuego();
        }
    }

    private void TerminarMinijuego()
    {
        Debug.Log($"Minijuego terminado. Puntos: {puntos}");

        portero.DetenerMovimiento();

        if (jugador != null && zonaSalidaJugador != null)
        {
            jugador.position = zonaSalidaJugador.position;
        }

        TicketsSystem.Instance.GanaTickets(puntos);
    }
}
