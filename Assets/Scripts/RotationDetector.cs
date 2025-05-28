using UnityEngine;

public class RotationDetector : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private float targetAngle = 270f;
    [SerializeField] private float tolerance = 5f; 
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private GameObject activateObject;

    private bool hasTriggered = false;

    private void Start()
    {
        activateObject.gameObject.SetActive(false);
    }

    void Update()
    {
        float currentZ = target.localEulerAngles.z;

        if (currentZ > 180f) currentZ -= 360f;

        if (Mathf.Abs(currentZ - targetAngle) <= tolerance)
        {
            if (!hasTriggered)
            {
                Debug.Log("¡Ángulo alcanzado!");
                OnAngleReached();

                if (triggerOnce) hasTriggered = true;
            }
        }
        else
        {
            if (!triggerOnce) hasTriggered = false;
        }
    }

    private void OnAngleReached()
    {
        activateObject.gameObject.SetActive(true);
        Debug.Log("Acción: premio, sonido, transición...");
    }
}
