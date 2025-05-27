using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class DuckManager : MonoBehaviour
{
    public Transform spawnPatos;
    public GameObject prefabPato;
    public GameObject pistola;
    public Transform puntoDisparo;
    public GameObject balaPrefab;

    public float cooldownDisparo = 1.5f;
    private bool puedeDisparar = true;

    public float duracionMinijuego = 30f;
    private float tiempoRestante;
    private bool enJuego = false;

    private int patosDerribados = 0;
    private List<GameObject> patosInstanciados = new List<GameObject>();

    [SerializeField] private ObstáculoMóvil obstaculo;
    public GameObject[] patos;

    [SerializeField] private ContinuousMoveProvider moveProvider;
    [SerializeField] private Transform jugador;
    [SerializeField] private Transform zonaSalidaJugador;

    [SerializeField] private GameObject esferaIzq;
    [SerializeField] private GameObject esferaDer;

    [SerializeField] private ParticleSystem disparoParticulas;

    public static DuckManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }

    public void EmpezarMinijuego()
    {
        if (enJuego) return;

        enJuego = true;
        tiempoRestante = duracionMinijuego;
        patosDerribados = 0;
        obstaculo.IniciarMovimiento();
        StartCoroutine(MinijuegoPorTiempo());

        moveProvider.enabled = false;

        esferaDer.gameObject.SetActive(true);
        esferaIzq.gameObject.SetActive(true);
    }

    IEnumerator MinijuegoPorTiempo()
    {
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < duracionMinijuego)
        {
            GameObject prefabAleatorio = patos[Random.Range(0, patos.Length)];
            GameObject pato = Instantiate(prefabAleatorio, spawnPatos.position, Quaternion.Euler(-90f, 100f, 180f));
            pato.GetComponent<Duck2>().manager = this;
            patosInstanciados.Add(pato);

            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }

        FinalizarMinijuego();
    }

    public void Disparar()
    {
        if (!enJuego || !puedeDisparar) return;

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        bala.GetComponent<Rigidbody>().linearVelocity = puntoDisparo.forward * 10f;

        puedeDisparar = false;
        StartCoroutine(ReactivarDisparo());

        Instantiate(disparoParticulas, puntoDisparo.transform);
    }

    IEnumerator ReactivarDisparo()
    {
        yield return new WaitForSeconds(cooldownDisparo);
        puedeDisparar = true;
    }

    public void NotificarPatoDerribado()
    {
        patosDerribados++;
    }

    public void FinalizarMinijuego()
    {
        enJuego = false;
        obstaculo.DetenerMovimiento();

        esferaDer.gameObject.SetActive(false);
        esferaIzq.gameObject.SetActive(false);

        MoverJugadorFuera();

        int puntos = patosDerribados;
        Debug.Log("Minijuego patos terminado. Puntos conseguidos: " + puntos);
        TicketsSystem.Instance.GanaTickets(puntos);

        TriggerInicio.Instance.started = false;

        foreach (var pato in patosInstanciados)
        {
            if (pato != null)
                Destroy(pato);
        }
        patosInstanciados.Clear();
    }

    private void MoverJugadorFuera()
    {
        if (jugador != null && zonaSalidaJugador != null)
            jugador.position = zonaSalidaJugador.position;

        moveProvider.enabled = true;

    }
}
