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

    private bool isPlaying = false;
    private bool noriaFinished = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnNoriaBegin?.Invoke(this, EventArgs.Empty);
            Debug.Log("Player Noria Detected");

            StartCoroutine(FinishNoria());

            FadeController.Instance.FadeIn();
            Invoke("FadeOut", 3f);

            isPlaying = true;
            noriaFinished = false;

            xrOrigin.transform.SetParent(noriaSeatTransform, false);
            xrOrigin.MoveCameraToWorldLocation(noriaSeatTransform.position);

            foreach (SplineAnimate spline in noriaSplinesAnimates)
            {
                if (spline != null)
                {
                    spline.Play();
                }
            }
        }
    }

    private void Update()
    {
        
    }

    IEnumerator FinishNoria()
    {
        yield return new WaitForSeconds(15f);

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
