using System.Collections;
using UnityEngine;

public class Ferrywheelmovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] cabinaGameObject;

    private bool canMoveCabin;

    private void Update()
    {
        if (canMoveCabin)
        {
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);

            foreach (Transform cabina in cabinaGameObject)
            {
                cabina.up = Vector3.up;
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            StartCoroutine(MoveCabin());
        }
    }

    private IEnumerator MoveCabin()
    {
        yield return new WaitForSeconds(3.0f);
        canMoveCabin = true;
    }


}
