using System;

namespace TicketBookingSystem.Models
{
    public class EconomyFlight : Flight
    {
        public override decimal CalculatePrice()
        {
            return BasePrice;
        }
    }
}
