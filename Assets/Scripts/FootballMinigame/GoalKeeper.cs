using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public float velocidadRotacion = 90f;
    private bool girando = false;

    void Update()
    {
        if (girando)
        {
            transform.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);
        }
    }

    public void IniciarMovimiento()
    {
        girando = true;
    }

    public void DetenerMovimiento()
    {
        girando = false;
    }
}
