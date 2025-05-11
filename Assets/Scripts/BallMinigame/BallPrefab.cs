using System;
using UnityEngine;

public class BallPrefab : MonoBehaviour
{
    public static BallPrefab Instance { get; private set; }

    [SerializeField] private Transform ballTransform;
    [SerializeField] private GameObject ballPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnBall()
    {
        Debug.Log("BallSpawned");
        GameObject newBall = Instantiate(ballPrefab, ballTransform.position, ballTransform.rotation);
    }
}
