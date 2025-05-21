using Unity.Hierarchy;
using UnityEngine;

public class TicketsSystem : MonoBehaviour
{
    public static TicketsSystem Instance { get; private set; }
    
    public int tickets = 0;
    private float rayDistance = 200;


    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            TicketShop ticketShop = hit.collider.GetComponent<TicketShop>();
            if (ticketShop != null )
            {
                Debug.Log($"Precio: {ticketShop.ticketCost} tickets.");
                //if (Input.GetKeyDown(KeyCode.J))
                //{
                    //ticketShop.TryPurchase(this);
                //}
            }


        }
    }

    public void GanaTickets(int ticketsGanados)
    {
        Debug.Log($"Tickets antes de sumar: {tickets}");
        tickets += ticketsGanados;
        Debug.Log($"Tickets sumados: {ticketsGanados}, tickets ahora: {tickets}");
    }

}
