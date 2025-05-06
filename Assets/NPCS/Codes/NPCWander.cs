using UnityEngine;
using UnityEngine.AI;

public class NPCWander : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    public float wanderRadius = 20f;
    public float wanderTimer = 5f;

    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }

        // Animación sincronizada con velocidad
        if (animator != null && agent != null)
        {
            float currentSpeed = agent.velocity.magnitude / agent.speed; // Normalizado entre 0 y 1
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
