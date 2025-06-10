using System;
using System.Collections;
using UnityEngine;

public class FootballScoreManager : MonoBehaviour
{
    public static FootballScoreManager Instance { get; private set; }

    [SerializeField] private float resetTime = 7f;

    private int FootballScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FootballScore = FootbalMinigameManager.Instance.puntos;
    }

    public IEnumerator ResetScores()
    {
        yield return new WaitForSeconds(resetTime);

        FootballScore = 0;
    }

    public int GetBestDucksScore()
    {
        return PlayerPrefs.GetInt("BestFootballScore", 0);
    }

    public void UpdateBestScore()
    {
        if (FootballScore > GetBestDucksScore())
        {
            PlayerPrefs.SetInt("BestFootballScore", FootballScore);
            PlayerPrefs.Save();
        }
    }
}
