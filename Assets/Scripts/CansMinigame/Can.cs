using UnityEngine;

public class Can : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Rigidbody rb;
    private bool yaContada = false;

    public CansManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!yaContada && other.CompareTag("Suelo"))
        {
            yaContada = true;
            manager.NotificarLataDerribada();
        }
    }

    public void Reiniciar()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        yaContada = false;
    }
}
