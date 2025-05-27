using UnityEngine;

public class TriggerCansMinigame : MonoBehaviour
{
    /* public CansManager manager;
    public string tagJugador = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            manager.EmpezarMinijuego();
        }
    } */

    public static TriggerCansMinigame Instance { get; private set; }

    public bool started = false;
    public bool playerInZone = false;

    [SerializeField] private DangerZoneEffect warningEffect;

    float maxTime = 5f;
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
            CansManager.Instance.EmpezarMinijuego();
            time = maxTime;
        }

        if (other.CompareTag("Player") && started)
        {
            playerInZone = true;
            time = maxTime;
            BackToGameCountDown.Instance.Hide();
            warningEffect.DisableWarningEffect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && started)
        {
            playerInZone = false;
            warningEffect.EnableWarningEffect();
            BackToGameCountDown.Instance.Show();
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
                StartCoroutine(CansManager.Instance.TerminarMinijuego());
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
