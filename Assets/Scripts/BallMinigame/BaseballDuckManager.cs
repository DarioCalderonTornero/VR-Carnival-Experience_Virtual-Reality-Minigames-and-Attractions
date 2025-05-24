using System.Collections.Generic;
using UnityEngine;

public class BaseballDuckManager : MonoBehaviour
{
    public static BaseballDuckManager Instance { get; private set; }

    [SerializeField] private ParticleSystem[] ps;

    private List<Duck> activeDuck = new List<Duck>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Duck.OnAnyDuckDestroyed += Duck_OnAnyDuckDestroyed;
    }

    private void OnDisable()
    {
        Duck.OnAnyDuckDestroyed -= Duck_OnAnyDuckDestroyed;
    }

    public void RegisterDuck(Duck duck)
    {
        activeDuck.Add(duck);
    }

    private void Duck_OnAnyDuckDestroyed(object sender, System.EventArgs e)
    {
        Duck duck = sender as Duck;

        if (duck != null)
        {
            activeDuck.Remove(duck);

            if (activeDuck.Count == 0)
            {
                foreach (var particleSystem in ps)
                {
                    particleSystem.Play();
                    BallManager.Instance.EndGame();
                }
                Debug.Log("All ducks eliminated");
            }
        }
    }
}