using System;
using System.Collections;
using UnityEngine;

public class DucksScoreManager : MonoBehaviour
{
    public static DucksScoreManager Instance { get; private set; }

    [SerializeField] private float resetTime = 7f;

    private int DucksScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        DucksScore = DuckManager.Instance.patosDerribados;
    }

    public IEnumerator ResetScores()
    {
        yield return new WaitForSeconds(resetTime);

        DucksScore = 0;
    }

    public int GetBestDucksScore()
    {
        return PlayerPrefs.GetInt("BestDucksScore", 0);
    }

    public void UpdateBestScore()
    {
        if (DucksScore > GetBestDucksScore())
        {
            PlayerPrefs.SetInt("BestDucksScore", DucksScore);
            PlayerPrefs.Save();
        }
    }
}
