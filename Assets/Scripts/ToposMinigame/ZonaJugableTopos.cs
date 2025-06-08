using UnityEngine;

public class ZonaJugableTopos : MonoBehaviour
{
    public static ZonaJugableTopos Instance { get; private set; }

    public bool started = false;
    public bool playerInZone = false;

    [SerializeField] private DangerZoneEffect warningEffect;

    [SerializeField] private float maxTime = 5f;
    float time;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !started)
        {
            started = true;
            playerInZone = true;
            //BallManager.Instance.StartGame();
            time = maxTime;
        }

        if (other.CompareTag("Player") && started)
        {
            playerInZone = true;
            time = maxTime;
            BackToGameCountDownTopos.Instance.Hide();
            warningEffect.DisableWarningEffect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && started)
        {
            playerInZone = false;
            warningEffect.EnableWarningEffect();
            BackToGameCountDownTopos.Instance.Show();
        }
    }

    private void Update()
    {
        if (!playerInZone && started)
        {
            time -= Time.deltaTime;

            Debug.Log(time);

            if (time > 0)
            {
                Debug.Log("CountDown");
            }

            if (time <= 0f)
            {
                TimerTopos.Instance.FinishMinigame();
                started = false;
                time = 0;
                warningEffect.DisableWarningEffect();
                BackToGameCountDown.Instance.Hide();
            }
        }
    }

    public float CurrentTime()
    {
        return time;
    }

    public void ForcePlayerInside()
    {
        playerInZone = true;
        time = 0f;
        Debug.Log("Player ya estaba dentro: zona validada manualmente.");
    }
}
