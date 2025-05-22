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
            Vector3 newPos = GetSafeRandomPoint(transform.position, wanderRadius);

            if (Vector3.Distance(transform.position, newPos) > 1.5f) // evita movimientos torpes
            {
                agent.SetDestination(newPos);
            }

            timer = 0;
        }

        // Animación sincronizada con velocidad
        if (animator != null && agent != null)
        {
            float currentSpeed = agent.velocity.magnitude / agent.speed;
            animator.SetFloat("Speed", currentSpeed);
        }
    }

    public static Vector3 GetSafeRandomPoint(Vector3 origin, float radius)
    {
        for (int i = 0; i < 30; i++) // hasta 30 intentos
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection.y = 0;
            Vector3 candidate = origin + randomDirection;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(candidate, out hit, 2.0f, NavMesh.AllAreas))
            {
                NavMeshPath path = new NavMeshPath();
                if (NavMesh.CalculatePath(origin, hit.position, NavMesh.AllAreas, path) &&
                    path.status == NavMeshPathStatus.PathComplete &&
                    Vector3.Distance(origin, hit.position) > 1.5f)
                {
                    return hit.position;
                }
            }
        }

        Debug.LogWarning("No se encontró un punto válido en la NavMesh. Usando posición original.");
        return origin;
    }
}
