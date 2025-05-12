using UnityEngine;

public class CanBalls : MonoBehaviour
{
    public CansManager manager;
    private bool yaNotificada = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!yaNotificada && collision.gameObject.tag != "Mesa")
        {
            yaNotificada = true;
            manager.NotificarPelotaLanzada();
        }
    }
}
