using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void EmpezarMinijuego()
    {
        if (enJuego) return;

        enJuego = true;
        tiempoRestante = duracionMinijuego;
        patosDerribados = 0;
        obstaculo.IniciarMovimiento();
        StartCoroutine(MinijuegoPorTiempo());
    }

    IEnumerator MinijuegoPorTiempo()
    {
        float tiempoInicio = Time.time;

        while (Time.time - tiempoInicio < duracionMinijuego)
        {
            GameObject prefabAleatorio = patos[Random.Range(0, patos.Length)];
            GameObject pato = Instantiate(prefabAleatorio, spawnPatos.position, Quaternion.Euler(-90f, 90f, 180f));
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

    void FinalizarMinijuego()
    {
        enJuego = false;
        obstaculo.DetenerMovimiento();

        int puntos = patosDerribados;
        Debug.Log("Minijuego terminado. Puntos: " + puntos);

        foreach (var pato in patosInstanciados)
        {
            if (pato != null)
                Destroy(pato);
        }
        patosInstanciados.Clear();
    }
}
