using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{


    [HideInInspector] public BallManager manager;
    private bool hasCollided = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collisioned");
        if (hasCollided) return;

        // Evitar reaccionar a la mesa donde aparece
        if (collision.collider.CompareTag("BallSpawnSurface")) return;

        if (collision.collider.CompareTag("Duck"))
        {
            Debug.Log("Duck Collisioned");
        }

        hasCollided = true;

        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        manager.OnBallDestroyed();
        Destroy(gameObject);
    }
}
