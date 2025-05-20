using System;
using System.Collections;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Splines;

public class NoriaManager : MonoBehaviour
{
    public static NoriaManager Instance { get; private set; }

    public event EventHandler OnNoriaBegin;

    [SerializeField] private SplineAnimate[] noriaSplinesAnimates;
    [SerializeField] private XROrigin xrOrigin;
    [SerializeField] private Transform noriaTransform;
    [SerializeField] private Transform noriaSeatTransform;
    [SerializeField] private Transform salidaTransform;

    [SerializeField] private float noriaTime = 10f;


    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnNoriaBegin?.Invoke(this, EventArgs.Empty);
            FadeController.Instance.FadeIn();

            StartCoroutine(BeginNoriaRide());

            foreach (SplineAnimate spline in noriaSplinesAnimates)
            {
                if (spline != null)
                    spline.Play();
            }
        }
    }

    private IEnumerator BeginNoriaRide()
    {
        yield return new WaitForSeconds(1.5f); 

        xrOrigin.transform.SetParent(null);

        xrOrigin.transform.position = noriaSeatTransform.position;
        xrOrigin.transform.rotation = noriaSeatTransform.rotation;

        xrOrigin.transform.SetParent(noriaSeatTransform, true);

        yield return new WaitForSeconds(1.5f);
        FadeOut();

       

        StartCoroutine(FinishNoria());
    }


    IEnumerator FinishNoria()
    {
        yield return new WaitForSeconds(noriaTime);

        FadeController.Instance.FadeIn();

        yield return new WaitForSeconds(3f);

        FadeOut();

        xrOrigin.transform.SetParent(null);
        xrOrigin.MoveCameraToWorldLocation(salidaTransform.position);

    }

    private void FadeOut()
    {
        FadeController.Instance.FadeOut();
    }
}