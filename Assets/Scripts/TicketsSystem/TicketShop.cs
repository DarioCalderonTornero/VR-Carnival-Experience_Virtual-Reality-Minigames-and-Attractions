using UnityEngine;

public class TicketShop : MonoBehaviour
{
    public int ticketCost;

    [SerializeField] private RideType rideType;

    private enum RideType
    {
        rollerCoaster, noria
    }

    public void TryPurchase(TicketsSystem player)
    {
        if (player.tickets >= ticketCost)
        {
            player.tickets -= ticketCost;

            switch(rideType)
            {
                case RideType.rollerCoaster:
                    RideRollerCoaster();
                    break;

                case RideType.noria:
                    RideNoria();
                    break;
            }
        }

        else
        {
            Debug.Log("Tickets insuficientes" + "sonido");
        }
    }

    private void RideRollerCoaster()
    {
        Debug.Log("RidingRollerCoaster");
    }

    private void RideNoria()
    {
        Debug.Log("Ride Noria");
    }
}
