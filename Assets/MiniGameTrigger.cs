using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public Transform hand;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            GameObject generador = GameObject.Find("Generador de globos");
            hand.gameObject.SetActive(true);
            BalloonSpawner spawner = generador.GetComponent<BalloonSpawner>();

            spawner.enabled = true;
        }
    }private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            GameObject generador = GameObject.Find("Generador de globos");

            BalloonSpawner spawner = generador.GetComponent<BalloonSpawner>();

            hand.gameObject.SetActive(false);

            spawner.enabled = false;
        }
    }
}
