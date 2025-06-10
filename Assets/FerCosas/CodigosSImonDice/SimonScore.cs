using UnityEngine;

public class SimonScore : MonoBehaviour
{
    public static SimonScore Instance { get; private set; }

    public int roundsCompleted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        roundsCompleted = 0;
    }

    public void AddRound()
    {
        roundsCompleted++;
        UpdateBestScore();
    }

    public int GetScore()
    {
        return roundsCompleted;
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestSimonScore", 0);
    }

    public void UpdateBestScore()
    {
        if (roundsCompleted > GetBestScore())
        {
            PlayerPrefs.SetInt("BestSimonScore", roundsCompleted);
            PlayerPrefs.Save();
        }
    }

    public void ResetScore()
    {
        roundsCompleted = 0;
    }
}
