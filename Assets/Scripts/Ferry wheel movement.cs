using System.Collections;
using UnityEngine;

public class Ferrywheelmovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] cabinaGameObject;

    private bool canMoveCabin;

    private void Awake()
    {
        StartCoroutine(MoveCabin());
    }

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

    

    private IEnumerator MoveCabin()
    {
        yield return new WaitForSeconds(0f);
        canMoveCabin = true;
    }


}
