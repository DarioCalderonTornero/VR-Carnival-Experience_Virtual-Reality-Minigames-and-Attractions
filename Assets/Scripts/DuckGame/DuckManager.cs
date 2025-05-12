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

    private int patosDisparados = 0;
    private int patosDerribados = 0;

    private List<GameObject> patosInstanciados = new List<GameObject>();

    void Start()
    {
        StartCoroutine(GenerarPatos());
    }

    IEnumerator GenerarPatos()
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject pato = Instantiate(prefabPato, spawnPatos.position, Quaternion.identity);
            pato.GetComponent<Duck>().manager = this;
            patosInstanciados.Add(pato);
            yield return new WaitForSeconds(Random.Range(0.8f, 1.5f)); 
        }

        yield return new WaitForSeconds(3f);
        FinalizarMinijuego();
    }

    public void Disparar()
    {
        if (!puedeDisparar) return;

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        bala.GetComponent<Rigidbody>().linearVelocity = puntoDisparo.forward * 20f;

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
        int puntos = patosDerribados == 6 ? 10 : patosDerribados;
        Debug.Log("Minijuego terminado. Puntos: " + puntos);

        // Reinicio opcional:
        foreach (var pato in patosInstanciados)
        {
            Destroy(pato);
        }
        patosInstanciados.Clear();
        patosDerribados = 0;

        StartCoroutine(GenerarPatos());
    }
}
