using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 localOffset = new Vector3(0f, 0f, 0.5f); 

    private void LateUpdate()
    {
        if (cameraTransform == null) 
            return;

        transform.position = cameraTransform.position +
                             cameraTransform.forward * localOffset.z +
                             cameraTransform.up * localOffset.y +
                             cameraTransform.right * localOffset.x;

        // Rotación para que mire en la misma dirección que el visor
        transform.rotation = Quaternion.LookRotation(cameraTransform.forward, cameraTransform.up);
    }
}
