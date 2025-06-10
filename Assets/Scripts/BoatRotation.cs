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

    private void Start()
    {
        rotacionOriginal = transform.rotation;
    }

    public void IniciarAtraccion(GameObject jugador)
    {
        if (!atraccionActiva)
            StartCoroutine(ActivarAtraccion(jugador));
    }

    public IEnumerator ActivarAtraccion(GameObject jugador)
    {
        FadeController.Instance.FadeIn();

        yield return new WaitForSeconds(1f);

        atraccionActiva = true;

        moveProvider.enabled = false;

        // Teletransportar al jugador
        jugador.transform.position = posicionInicioJugador.position;
        jugador.transform.rotation = posicionInicioJugador.rotation;
        jugador.transform.SetParent(posicionInicioJugador);

        FadeController.Instance.FadeOut();

        yield return new WaitForSeconds(1f);

        // Iniciar la rotación del barco
        RotarBarco();

        // Esperar la duración de la atracción
        yield return new WaitForSeconds(duracionAtraccion);

        // Finalizar la atracción con coroutine
        yield return StartCoroutine(FinalizarAtraccion());
    }

    void RotarBarco()
    {
        Vector3 rotacionInicial = transform.rotation.eulerAngles;

        secuencia = DOTween.Sequence();

        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, maxRotationAngle), tiempoSubida, RotateMode.FastBeyond360)
            .SetEase(Ease.InQuad));

        secuencia.Append(transform.DORotate(new Vector3(rotacionInicial.x, rotacionInicial.y, -maxRotationAngle), tiempoSubida, RotateMode.FastBeyond360)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo));

        secuencia.SetLoops(-1, LoopType.Restart);
    }

    public IEnumerator FinalizarAtraccion()
    {
        if (secuencia != null && secuencia.IsActive()) secuencia.Kill();
        transform.DORotateQuaternion(rotacionOriginal, 1f);

        FadeController.Instance.FadeIn();

        yield return new WaitForSeconds(1f);

        // Teletransportar al jugador a la salida
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            jugador.transform.SetParent(null);

            jugador.transform.position = posicionSalidaJugador.position;
            jugador.transform.rotation = posicionSalidaJugador.rotation;
        }

        atraccionActiva = false;

        FadeController.Instance.FadeOut();

        moveProvider.enabled = true;
        
    }
}
