using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using DOTweenSequence = DG.Tweening.Sequence;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using System.Collections;

public class BoatRotation : MonoBehaviour
{
    public float maxRotationAngle = 60f;
    public float tiempoSubida = 2f;
    public float duracionAtraccion = 10f;

    public Transform posicionInicioJugador;
    public Transform posicionSalidaJugador;

    private Quaternion rotacionOriginal;
    private DOTweenSequence secuencia;
    private bool atraccionActiva = false;

    public ContinuousMoveProvider moveProvider;

    private GameObject jugadorActual;

    private void Start()
    {
        rotacionOriginal = transform.rotation;
    }

    public void IniciarAtraccion(GameObject jugador)
    {
        if (!atraccionActiva)
        {
            jugadorActual = jugador;
            StartCoroutine(ActivarAtraccion());
        }
    }

    private IEnumerator ActivarAtraccion()
    {
        FadeController.Instance.FadeIn();
        yield return new WaitForSeconds(1f);

        atraccionActiva = true;
        moveProvider.enabled = false;

        // Teletransportar al jugador
        jugadorActual.transform.position = posicionInicioJugador.position;
        jugadorActual.transform.rotation = posicionInicioJugador.rotation;

        // Hacer al jugador hijo del barco para que rote con él
        jugadorActual.transform.SetParent(transform);

        FadeController.Instance.FadeOut();
        yield return new WaitForSeconds(1f);

        // Iniciar la rotación del barco
        RotarBarco();

        // Esperar la duración de la atracción
        yield return new WaitForSeconds(duracionAtraccion);

        // Finalizar la atracción
        yield return StartCoroutine(FinalizarAtraccion());
    }

    private void RotarBarco()
    {
        Vector3 rotacionInicial = transform.rotation.eulerAngles;

        secuencia = DOTween.Sequence();
        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, maxRotationAngle), tiempoSubida, RotateMode.FastBeyond360)
            .SetEase(Ease.InQuad));

        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, -maxRotationAngle), tiempoSubida, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo)); // SOLO un SetLoops aquí
    }

    public IEnumerator FinalizarAtraccion()
    {
        if (secuencia != null && secuencia.IsActive()) secuencia.Kill();

        // Volver a la rotación original
        transform.DORotateQuaternion(rotacionOriginal, 1f);

        FadeController.Instance.FadeIn();
        yield return new WaitForSeconds(1f);

        if (jugadorActual != null)
        {
            // Quitar como hijo y mover a la salida
            jugadorActual.transform.SetParent(null);
            jugadorActual.transform.position = posicionSalidaJugador.position;
            jugadorActual.transform.rotation = posicionSalidaJugador.rotation;
        }

        atraccionActiva = false;
        jugadorActual = null;

        FadeController.Instance.FadeOut();
        moveProvider.enabled = true;
    }
}
