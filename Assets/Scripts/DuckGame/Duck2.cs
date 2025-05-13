using UnityEngine;

public class Duck2 : MonoBehaviour
{
    public bool derribado = false;
    public DuckManager manager;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.right * 2f; 
    }

    public void Tumbar()
    {
        derribado = true;
        manager.NotificarPatoDerribado();
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.AddForce(Vector3.down * 3f, ForceMode.Impulse);
        transform.Rotate(Vector3.forward * 90f); 
    }
}
