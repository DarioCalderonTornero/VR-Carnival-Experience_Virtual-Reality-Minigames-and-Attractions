using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BalloonFloat : MonoBehaviour
{
    [Header("Movimiento flotante")]
    public float floatSpeed = 1.5f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;
    Ballon_Score ballon_Score;

    [Header("FX al explotar")]
    public GameObject explosionFX;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        Destroy(gameObject, 15f); 
    }

    void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        
        float offsetX = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x + offsetX, transform.position.y, startPos.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
          Instantiate(explosionFX, transform.position, Quaternion.identity);
          ballon_Score.score_balloon++;
          Destroy(gameObject);
        }
    }
}
