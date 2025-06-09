using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ToposScoreManager : MonoBehaviour
{
    public static ToposScoreManager Instance { get; private set; }

    public event EventHandler OnGoldTopo;
    public event EventHandler OnRedTopo;
    public event EventHandler OnNormalTopo;

    public int normalScore, goldScore, redScore;

    [SerializeField] private float resetTime = 7f;

    private int toposScore;

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

        toposScore = 0;
        normalScore = 0;
        goldScore = 0;
        redScore = 0;
    }

    public int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestToposScore", 0);
    }

    public void UpdateBestScore()
    {
        if (toposScore > GetBestScore())
        {
            PlayerPrefs.SetInt("BestToposScore", toposScore);
            PlayerPrefs.Save();
        }
    }

    public void ScoreRegister(Topo topo)
    {
        switch (topo.topoType)
        {
            case Topo.TopoType.normal:
                toposScore++;
                normalScore++;
                OnNormalTopo?.Invoke(this, EventArgs.Empty);
                break;

            case Topo.TopoType.gold:
                toposScore += 3;
                goldScore++;
                OnGoldTopo?.Invoke(this, EventArgs.Empty);
                break;

            case Topo.TopoType.red:
                toposScore -= 1;
                redScore++;
                OnRedTopo?.Invoke(this, EventArgs.Empty);
                break;
        }
    }

    public int GetRedScore()
    {
        return redScore;
    }

    public int GetNormalScore()
    {
        return normalScore;
    }

    public int GetGoldScore()
    {
        return goldScore;
    }

    public int GetCurrentScore()
    {
        return toposScore;
    }
}
