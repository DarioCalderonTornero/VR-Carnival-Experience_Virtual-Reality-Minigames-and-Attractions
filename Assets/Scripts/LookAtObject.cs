using Unity.VisualScripting;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{

    [SerializeField] private Transform lookTarget;

    private void Update()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        if (lookTarget == null)
            return;

        Vector3 direction = lookTarget.position - transform.position;

        direction.z = 0f;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }
}
