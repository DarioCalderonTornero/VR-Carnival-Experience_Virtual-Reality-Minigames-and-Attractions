using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Splines;

public class RollerCoasterManager : MonoBehaviour
{
    [SerializeField] private SplineAnimate rollercoasterSpline;
    [SerializeField] private Transform player;
    [SerializeField] private Transform rollerCoaster;
    [SerializeField] private Transform seatTransform;
    [SerializeField] private Transform salidaTransform;

    private bool splineStarted = false;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if (splineStarted && !rollercoasterSpline.IsPlaying && rollercoasterSpline.NormalizedTime >= 1)
        {
            FadeController.Instance.FadeIn();

            Invoke("FadeOut", 3f);

            StartCoroutine(FinishRollerCoaster());

            Debug.Log("Bajar Player");
        }
    }

    private IEnumerator FinishRollerCoaster()
    {
        yield return new WaitForSeconds(3f);

        splineStarted = false;
        player.SetParent(null);

        player.transform.position = salidaTransform.transform.position;
        player.transform.rotation = salidaTransform.transform.rotation;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");

            FadeController.Instance.FadeIn();
            Invoke("FadeOut", 3f);

            splineStarted = true;

            player.SetParent(rollerCoaster);

            player.localPosition = seatTransform.localPosition;
            player.localRotation = seatTransform.localRotation;

            rollercoasterSpline.Play();         
        }
    }

    private void FadeOut()
    {
        FadeController.Instance.FadeOut();
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("TP Player");
    }
}
