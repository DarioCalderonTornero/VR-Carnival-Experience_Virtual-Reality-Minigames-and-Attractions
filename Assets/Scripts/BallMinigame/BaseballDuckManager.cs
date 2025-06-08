using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseballDuckManager : MonoBehaviour
{
    public static BaseballDuckManager Instance { get; private set; }

    public event EventHandler OnAllDucksDestroyed;

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
        if (!activeDuck.Contains(duck))
        {
            activeDuck.Add(duck);
            Debug.Log("Duck registrado. Total: " + activeDuck.Count);
        }
    }

    public void ResetDuckList()
    {
        activeDuck.Clear();
        Debug.Log("Lista de patos reseteada");
    }

    private void Duck_OnAnyDuckDestroyed(object sender, System.EventArgs e)
    {
        Duck duck = sender as Duck;

        if (duck != null)
        {
            activeDuck.Remove(duck);
            Debug.Log("Pato eliminado. Quedan: " + activeDuck.Count);

            if (activeDuck.Count == 0)
            {
                foreach (var particleSystem in ps)
                {
                    particleSystem.Play();
                }

                OnAllDucksDestroyed?.Invoke(this, EventArgs.Empty);
                BallManager.Instance.EndGame();
            }
        }
    }
}
