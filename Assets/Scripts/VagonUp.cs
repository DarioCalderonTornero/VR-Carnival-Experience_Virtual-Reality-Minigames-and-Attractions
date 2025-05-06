using UnityEngine;

public class VagonUp : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.up = Vector3.up;
    }
}
