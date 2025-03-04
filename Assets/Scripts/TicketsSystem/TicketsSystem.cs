using UnityEngine;

public class TicketsSystem : MonoBehaviour
{
    public int tickets;
    private float rayDistance;
    private bool interactingWithTickets = false;

    public enum interactTypes
    {
        case1,
        case2,
    }

    [SerializeField] private interactTypes currentInteraction;

    void Update()
    {
        tickets++;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Tickets"))
            {
                Debug.Log("Interacted with tickets");
            }
        }
    }

    private void GetCase1()
    {
        currentInteraction = interactTypes.case1;
    }

    private void GetCase2()
    {
        currentInteraction = interactTypes.case2;
    }

    private interactTypes ReturnCurrentInteraction()
    {
        return currentInteraction;
    }


}
