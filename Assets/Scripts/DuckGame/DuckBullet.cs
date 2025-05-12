using UnityEngine;

public class DuckBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pato"))
        {
            Duck pato = collision.gameObject.GetComponent<Duck>();
            if (pato != null && !pato.derribado)
            {
                pato.Tumbar();
            }
        }

        Destroy(gameObject); 
    }
}
