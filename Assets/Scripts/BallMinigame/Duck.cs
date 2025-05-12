using System;
using System.Collections;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public static Duck Instance { get; private set; }

    public event EventHandler OnBallDetected;

    private void Awake()
    {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("DuckDetected");
            OnBallDetected?.Invoke(this, EventArgs.Empty);
            StartCoroutine(DestroyDuck());
        }
    }

    private IEnumerator DestroyDuck()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
