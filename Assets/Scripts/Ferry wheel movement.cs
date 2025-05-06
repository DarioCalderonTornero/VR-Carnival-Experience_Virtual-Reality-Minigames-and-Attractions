using System.Collections;
using UnityEngine;

public class Ferrywheelmovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform[] cabinaGameObject;

    private bool canMoveCabin;

    private void Start()
    {
        NoriaManager.Instance.OnNoriaBegin += NoriaManager_OnNoriaBegin;
    }

    private void NoriaManager_OnNoriaBegin(object sender, System.EventArgs e)
    {
        canMoveCabin = true;
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

}
