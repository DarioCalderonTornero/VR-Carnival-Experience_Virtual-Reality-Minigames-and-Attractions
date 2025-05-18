using UnityEngine;

public class Duck2 : MonoBehaviour
{
    public bool derribado = false;
    public DuckManager manager;
    private Rigidbody rb;
    public Vector3 destino;
    public float velocidad;

    public float ValorX;
    public float ValorY;
    public float ValorZ;

    void Start()
    {
        velocidad = Random.Range(0.8f, 1f);
        rb = GetComponent<Rigidbody>();
        destino = new Vector3(ValorX, ValorY, ValorZ);  
    }

    void Update()
    {
        if (!derribado)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

            if (transform.position == destino)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Tumbar()
    {
        derribado = true;
        manager.NotificarPatoDerribado();
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = true;
        rb.AddForce(Vector3.back * 7f, ForceMode.Impulse);
        // transform.Rotate(Vector3.forward * 90f);
    }
}
