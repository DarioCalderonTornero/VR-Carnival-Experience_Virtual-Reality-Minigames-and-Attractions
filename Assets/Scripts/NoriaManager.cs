using UnityEngine;
using UnityEngine.Splines;

public class NoriaManager : MonoBehaviour
{
    [SerializeField] private SplineAnimate[] noriaSplinesAnimates;
    [SerializeField] private Transform noriaTransform;
    [SerializeField] private Transform noriaSeatTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Noria Detected");

            FadeController.Instance.FadeIn();


        }
    }
}
