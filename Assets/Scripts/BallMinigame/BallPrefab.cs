using System;
using UnityEngine;

public class BallPrefab : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BallManager manager;

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        Ball ball = newBall.GetComponent<Ball>();
        ball.manager = manager;
    }
}
