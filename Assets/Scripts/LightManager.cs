using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightManager : MonoBehaviour
{
    public int numeroSerie = 1;
    private List<Color> colores = new List<Color>();
    private Light luz;
    private int colorIndex = 0;

    void Start()
    {
        luz = GetComponent<Light>();
        SetColoresPorSerie(numeroSerie);
        StartCoroutine(CambiarColorCadaSegundo());
    }

    void SetColoresPorSerie(int serie)
    {
        colores.Clear();

        switch (serie)
        {
            case 1:
                colores.Add(Color.red);
                colores.Add(Color.yellow);
                colores.Add(Color.green);
                break;
            case 2:
                colores.Add(Color.blue);
                colores.Add(Color.cyan);
                colores.Add(Color.magenta);
                break;
            case 3:
                colores.Add(new Color(1f, 0.5f, 0f)); // naranja
                colores.Add(new Color(0.5f, 0f, 1f)); // púrpura
                colores.Add(new Color(0f, 1f, 0.5f)); // verde menta
                break;
            case 4:
                colores.Add(Color.red);
                colores.Add(Color.blue);
                colores.Add(Color.white);
                break;
            default:
                colores.Add(Color.white);
                break;
        }
    }

    IEnumerator CambiarColorCadaSegundo()
    {
        while (true)
        {
            luz.color = colores[colorIndex];
            colorIndex = (colorIndex + 1) % colores.Count;
            yield return new WaitForSeconds(1f);
        }
    }
}
