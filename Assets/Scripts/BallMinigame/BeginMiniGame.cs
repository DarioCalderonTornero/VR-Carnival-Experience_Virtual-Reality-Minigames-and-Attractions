using System;
using UnityEngine;

public class BeginMiniGame : MonoBehaviour
{
    public static BeginMiniGame Instance { get; private set; }

    public event EventHandler OnBeginBaseBall;

    public bool started = false;
    public bool playerInZone = false;

    float maxTime = 5f;
    float time;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !started)
        {
            started = true;
            playerInZone = true;
            BallManager.Instance.StartGame();
            time = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && started)
        {
            playerInZone = false;      
        }
    }

    private void Update()
    {
        if (!playerInZone && started)
        {
            time += Time.deltaTime;

            Debug.Log(time);

            if (time < maxTime)
            {
                Debug.Log("CountDown");
            }

            if (time >= maxTime)
            {
                BallManager.Instance.EndGame();
                started = false;
                time = 0;
            }
        }       
    }
}
