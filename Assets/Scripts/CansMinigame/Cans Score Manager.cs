using System;
using System.Collections;
using UnityEngine;

public class CansScoreManager : MonoBehaviour
{
    public static CansScoreManager Instance { get; private set; }

    // public event EventHandler OnGoldTopo;
    // public event EventHandler OnRedTopo;
    // public event EventHandler OnNormalTopo;

    [SerializeField] private float resetTime = 7f;

    private int CansScore;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Topo.OnAnyTopoDestroyed += Topo_OnAnyTopoDestroyed;
        TimerTopos.Instance.OnTimerFinish += TimerTopos_OnTimerFinish;
    }

    private void TimerTopos_OnTimerFinish(object sender, EventArgs e)
    {
        UpdateBestScore();
        StartCoroutine(ResetScores());
    }

    private IEnumerator ResetScores()
    {
        yield return new WaitForSeconds(resetTime);

        CansScore = 0;
    }

    public int GetBestCansScore()
    {
        return PlayerPrefs.GetInt("BestCansScore", 0);
    }

    public void UpdateBestScore()
    {
        if (CansScore > GetBestCansScore())
        {
            PlayerPrefs.SetInt("BestCansScore", CansScore);
            PlayerPrefs.Save();
        }
    }
}
