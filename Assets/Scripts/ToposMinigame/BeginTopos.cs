using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

public class BeginTopos : MonoBehaviour
{
    [SerializeField] private GameObject[] hammerGameObjects;

    private void Start()
    {
        HideHammers();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Begin Topos");
            ShowHammers();
        }
    }

    private void ShowHammers()
    {
        foreach (var ham in hammerGameObjects)
        {
            ham.gameObject.SetActive(true);
        }
    }

    private void HideHammers()
    {
        foreach (var ham in hammerGameObjects)
        {
            ham.gameObject.SetActive(false);
        }
    }
}
