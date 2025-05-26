using UnityEditor.EditorTools;
using UnityEngine;

public class GoslBall : MonoBehaviour
{
    private FootbalMinigameManager manager;
    private bool yaNotificado = false;

    public void AsignarManager(FootbalMinigameManager m)
    {
        manager = m;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!yaNotificado && collision.gameObject.tag != "Mesa")
        {
            yaNotificado = true;
            manager.PelotaTocadaNoMesa();
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gol"))
        {
            yaNotificado = true;
            manager.GolMarcado();
            Destroy(gameObject, 2f);
        }
    }
}
