using UnityEngine;

public class Ballon_Score : MonoBehaviour
{
    public static Ballon_Score Instance { get; private set; }

    public int score_balloon;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        score_balloon = 0;
    }


    public int GetScore()
    {
        return score_balloon;
    }
    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestBalloonScore", 0);
    }
    public void UpdateBestScore()
    {
        if (score_balloon > GetBestScore())
        {
            PlayerPrefs.SetInt("BestBalloonScore", score_balloon);
            PlayerPrefs.Save();
        }
    }
}
