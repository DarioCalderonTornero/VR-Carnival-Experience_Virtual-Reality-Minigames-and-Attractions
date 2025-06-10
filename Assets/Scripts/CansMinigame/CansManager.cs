using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class CansManager : MonoBehaviour
{
    [Header("Pelotas")]
    public GameObject pelotaPrefab;
    public Transform[] posicionesPelotas;
    [SerializeField] private GameObject triggerBolas;

    [Header("Latas")]
    public List<Can> latas;

    [Header("Estructuras de Latas")]
    public GameObject[] estructurasLatasPrefabs; 

    [Header("Zona de juego")]
    public Transform zonaInicialJugador;
    public Transform zonaSalidaJugador;

    [Header("Jugador")]
    public Transform jugador;

    [Header("Puntos")]
    public int latasCaidasTotales = 0;

    private List<GameObject> pelotasInstanciadas = new List<GameObject>();
    private int pelotasUsadas = 0;
    private bool enJuego = false;

    [SerializeField] private ContinuousMoveProvider moveProvider;

    [Header("Temporizador")]
    public float tiempoLimite = 30f; 
    private float tiempoRestante;
    private bool temporizadorActivo = false;

    [Header("Posición de las estructuras")]
    public Transform zonaEstructuraLatas;

    private bool minijuegoTerminado = false;

    public static CansManager Instance { get; private set; }

    [Header("Rondas")]
    [SerializeField] private int rondasTotales = 5;
    private int rondasActuales = 0;

    private void Awake()
    {
        Instance = this;
    }

    /*private void Update()
    {
        if (enJuego && temporizadorActivo && !minijuegoTerminado)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0f)
            {
                minijuegoTerminado = true;
                // StartCoroutine(TerminarMinijuego());
            }
        }
    }*/

    public void EmpezarMinijuego()
    {
        if (enJuego) return;

        rondasActuales = 0;

        // tiempoRestante = tiempoLimite;
        // temporizadorActivo = true;

        enJuego = true;
        pelotasUsadas = 0;
        latasCaidasTotales = 0;

        foreach (var lata in latas)
            lata.Resetear();

        foreach (var pelota in pelotasInstanciadas)
        {
            Destroy(pelota);  
        }
        pelotasInstanciadas.Clear();

        StartCoroutine(InstanciarBolas());
        StartCoroutine(GenerarEstructuraAleatoria());

        Debug.Log("Minijuego iniciado.");
    }

    IEnumerator GenerarEstructuraAleatoria()
    {
        yield return new WaitForSeconds(1f);

        // Eliminar latas anteriores
        foreach (var lata in latas)
        {
            if (lata != null && lata.gameObject != null)
            {
                Destroy(lata.gameObject);
            }
        }
        latas.Clear();

        // Eliminar estructura anterior (si existe algo dentro)
        foreach (Transform child in zonaEstructuraLatas)
        {
            Destroy(child.gameObject);
        }

        // Elegir estructura aleatoria y colocarla en la zona correcta
        int indexAleatorio = Random.Range(0, estructurasLatasPrefabs.Length);
        GameObject estructuraSeleccionada = estructurasLatasPrefabs[indexAleatorio];

        // Instanciar como hija de zonaEstructuraLatas y resetear su posición local
        GameObject nuevaEstructura = Instantiate(estructuraSeleccionada, zonaEstructuraLatas);
        nuevaEstructura.transform.localPosition = Vector3.zero;
        nuevaEstructura.transform.localRotation = Quaternion.identity;

        // Registrar las latas instanciadas
        latas.AddRange(nuevaEstructura.GetComponentsInChildren<Can>());

        StartCoroutine(DestruirBolas());
    }

    public void PelotaLanzada()
    {
        pelotasUsadas++;
        if (pelotasUsadas >= 3)
        {
            pelotasUsadas = 0;
            rondasActuales++;

            if (rondasActuales >= rondasTotales)
            {
                StartCoroutine(TerminarMinijuego());
            }
            else
            {
                StartCoroutine(GenerarEstructuraAleatoria());
                StartCoroutine(InstanciarBolas());
            }
        }
    }

    public void LataCaida()
    {
        latasCaidasTotales++; 
    }

    public IEnumerator TerminarMinijuego()
    {
        int puntos = latasCaidasTotales;
        Debug.Log($"Minijuego terminado. Puntos totales: {puntos}");

        CansScoreManager.Instance.UpdateBestScore();
        StartCoroutine(CansScoreManager.Instance.ResetScores());

        StartCoroutine(CansFinalScore.Instance.Show());

        yield return new WaitForSeconds(7f);

        FadeController.Instance.FadeIn();
        TriggerCansMinigame.Instance.started = false;

        yield return new WaitForSeconds(1f);

        MoverJugadorFuera();

        yield return new WaitForSeconds(1f);

        FadeController.Instance.FadeOut();

        enJuego = false;
        temporizadorActivo = false;

        TicketsSystem.Instance.GanaTickets(puntos);

        minijuegoTerminado = false;

        yield return null;
    }

    private void MoverJugadorFuera()
    {
        if (jugador != null && zonaSalidaJugador != null)
            jugador.position = zonaSalidaJugador.position;

        foreach (var pelota in pelotasInstanciadas)
        {
            Destroy(pelota);  
        }
        pelotasInstanciadas.Clear();
    }

    IEnumerator DestruirBolas()
    {
        triggerBolas.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        triggerBolas.gameObject.SetActive(false);
    }

    IEnumerator InstanciarBolas()
    {
        yield return new WaitForSeconds(1f);
        foreach (var pos in posicionesPelotas)
        {
            var pelota = Instantiate(pelotaPrefab, pos.position, Quaternion.identity);
            pelota.GetComponent<CanBalls>().AsignarManager(this);
            pelotasInstanciadas.Add(pelota);
        }
    }

}
