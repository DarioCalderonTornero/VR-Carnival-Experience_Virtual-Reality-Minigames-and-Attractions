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
    private int latasCaidasTotales = 0;

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

    private void Update()
    {
        if (enJuego && temporizadorActivo)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0f)
            {
                StartCoroutine(TerminarMinijuego());
            }
        }
    }

    public void EmpezarMinijuego()
    {
        if (enJuego) return;

        tiempoRestante = tiempoLimite;
        temporizadorActivo = true;

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
        
        foreach (var lata in latas)
        {
            if (lata != null && lata.gameObject != null) 
            {
                Destroy(lata.gameObject); 
            }
        }
        latas.Clear();

        if (zonaEstructuraLatas != null && zonaEstructuraLatas.gameObject != null)
        {
            Destroy(zonaEstructuraLatas.gameObject); 
        }
        else
        {
            Debug.LogWarning("El contenedor de latas ya ha sido destruido o no existe.");
        }

        int indexAleatorio = Random.Range(0, estructurasLatasPrefabs.Length);
        GameObject estructuraSeleccionada = estructurasLatasPrefabs[indexAleatorio];

        GameObject nuevaEstructura = Instantiate(estructuraSeleccionada, zonaEstructuraLatas.position, Quaternion.identity);

        zonaEstructuraLatas = nuevaEstructura.transform;

        latas.AddRange(nuevaEstructura.GetComponentsInChildren<Can>());  

        StartCoroutine(DestruirBolas());
    }

public void PelotaLanzada()
    {
        pelotasUsadas++;
        if (pelotasUsadas >= 3)
        {
            StartCoroutine(GenerarEstructuraAleatoria());
            pelotasUsadas = 0; 

            StartCoroutine(InstanciarBolas());
        }
    }

    public void LataCaida()
    {
        latasCaidasTotales++; 
    }

    private IEnumerator TerminarMinijuego()
    {
        int puntos = latasCaidasTotales;
        Debug.Log($"Minijuego terminado. Puntos totales: {puntos}");

        MoverJugadorFuera();

        
        enJuego = false;
        temporizadorActivo = false;

        yield return null;
    }

    private void MoverJugadorFuera()
    {
        if (jugador != null && zonaSalidaJugador != null)
            jugador.position = zonaSalidaJugador.position;

        moveProvider.enabled = true;

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
