using System;
using System.Collections;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public static Duck Instance { get; private set; }

    public static event EventHandler OnAnyDuckDetected;
    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("DuckDetected");
            OnAnyDuckDetected?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DestroyDuck());
        }
    }

    private IEnumerator DestroyDuck()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
