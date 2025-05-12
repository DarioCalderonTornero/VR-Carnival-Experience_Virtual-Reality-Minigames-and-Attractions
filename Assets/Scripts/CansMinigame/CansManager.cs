using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CansManager : MonoBehaviour
{
    public Transform[] posicionesPelotas;
    public GameObject pelotaPrefab;

    public List<Can> latas; 

    private int pelotasLanzadas = 0;
    private int latasDerribadas = 0;
    private List<GameObject> pelotasInstanciadas = new List<GameObject>();

    public Transform mesaPelotas;

    void Start()
    {
        IniciarMinijuego();
    }

    public void IniciarMinijuego()
    {
        pelotasLanzadas = 0;
        latasDerribadas = 0;

        foreach (var lata in latas)
        {
            lata.Reiniciar();
        }

        foreach (var p in pelotasInstanciadas)
        {
            Destroy(p);
        }
        pelotasInstanciadas.Clear();

        for (int i = 0; i < posicionesPelotas.Length; i++)
        {
            GameObject pelota = Instantiate(pelotaPrefab, posicionesPelotas[i].position, Quaternion.identity);
            pelota.GetComponent<CanBalls>().manager = this;
            pelotasInstanciadas.Add(pelota);
        }
    }

    public void NotificarPelotaLanzada()
    {
        pelotasLanzadas++;
        if (pelotasLanzadas >= 3)
        {
            StartCoroutine(FinalizarMinijuegoTrasEspera());
        }
    }

    public void NotificarLataDerribada()
    {
        latasDerribadas++;
    }

    IEnumerator FinalizarMinijuegoTrasEspera()
    {
        yield return new WaitForSeconds(5f);

        int puntos = latasDerribadas == 6 ? 10 : latasDerribadas;
        Debug.Log($"¡Fin del minijuego! Puntos ganados: {puntos}");

        IniciarMinijuego();
    }
}
