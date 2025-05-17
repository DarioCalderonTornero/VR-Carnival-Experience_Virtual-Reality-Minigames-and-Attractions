using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public static event Action OnHammerTriggered;
    public bool hasStarted;

    public void HanldeGrab()
    {
        if (hasStarted)
            return;

        hasStarted = true;

        OnHammerTriggered?.Invoke();

        //ToposManager.Instance.StartToposMinigame();
    }
}
