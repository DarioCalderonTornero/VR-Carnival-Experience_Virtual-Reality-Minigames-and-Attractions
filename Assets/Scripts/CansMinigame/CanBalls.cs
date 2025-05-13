using UnityEngine;

public class CanBalls : MonoBehaviour
{
    private CansManager manager;
    private bool notificada = false;

    public void AsignarManager(CansManager m)
    {
        manager = m;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!notificada && collision.gameObject.tag != "Mesa")
        {
            notificada = true;
            manager?.PelotaLanzada();
        }
    }
}
