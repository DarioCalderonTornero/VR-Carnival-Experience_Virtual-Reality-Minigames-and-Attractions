using UnityEngine;

public class DuckBullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pato"))
        {
            Duck2 pato = collision.gameObject.GetComponent<Duck2>();
            if (pato != null && !pato.derribado)
            {
                pato.Tumbar();
            }
        }

        Destroy(gameObject); 
    }
}
