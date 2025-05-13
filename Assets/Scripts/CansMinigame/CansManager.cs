using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CansManager : MonoBehaviour
{
    [Header("Pelotas")]
    public GameObject pelotaPrefab;
    public Transform[] posicionesPelotas;

    [Header("Latas")]
    public List<Can> latas;

    [Header("Zona de juego")]
    public Transform zonaInicialJugador;
    public Transform zonaSalidaJugador;

    [Header("Jugador")]
    public Transform jugador;

    private List<GameObject> pelotasInstanciadas = new List<GameObject>();
    private int pelotasUsadas = 0;
    private int latasCaidas = 0;
    private bool enJuego = false;
    private void Start()
    {
        EmpezarMinijuego();
    }
    public void EmpezarMinijuego()
    {
        if (enJuego) return;

        enJuego = true;
        pelotasUsadas = 0;
        latasCaidas = 0;

        // Resetear latas
        foreach (var lata in latas)
            lata.Resetear();

        // Eliminar pelotas previas
        foreach (var pelota in pelotasInstanciadas)
            Destroy(pelota);
        pelotasInstanciadas.Clear();

        // Instanciar pelotas nuevas
        foreach (var pos in posicionesPelotas)
        {
            var pelota = Instantiate(pelotaPrefab, pos.position, Quaternion.identity);
            pelota.GetComponent<CanBalls>().AsignarManager(this);
            pelotasInstanciadas.Add(pelota);
        }

        Debug.Log("Minijuego iniciado.");
    }

    public void PelotaLanzada()
    {
        pelotasUsadas++;
        if (pelotasUsadas >= 3)
            StartCoroutine(TerminarTrasEspera());
    }

    public void LataCaida()
    {
        latasCaidas++;
    }

    private IEnumerator TerminarTrasEspera()
    {
        yield return new WaitForSeconds(5f);

        int puntos = latasCaidas == latas.Count ? 10 : latasCaidas;
        Debug.Log($"Minijuego terminado. Puntos: {puntos}");

        MoverJugadorFuera();
        enJuego = false;
    }

    private void MoverJugadorFuera()
    {
        if (jugador != null && zonaSalidaJugador != null)
            jugador.position = zonaSalidaJugador.position;
    }
}
