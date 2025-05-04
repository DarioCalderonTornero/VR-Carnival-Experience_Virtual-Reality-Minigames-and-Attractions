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

    private void Start()
    {
        StartCoroutine(Delay());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");

            player.SetParent(rollerCoaster);

            player.localPosition = seatTransform.localPosition;
            player.localRotation = seatTransform.localRotation;

            rollercoasterSpline.Play();
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("TP Player");
        

    }
}
