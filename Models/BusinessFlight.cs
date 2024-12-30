using System;

namespace TicketBookingSystem.Models
{
    public class BusinessFlight : Flight
    {
        public override decimal CalculatePrice()
        {
            return BasePrice * 2;
        }
    }
}
