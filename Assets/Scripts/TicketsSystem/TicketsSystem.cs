using UnityEngine;

public class TicketsSystem : MonoBehaviour
{
    public int tickets;
    private float rayDistance = 200;

    void Update()
    {
        tickets++;
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

}
