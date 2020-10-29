using System;
using System.Collections.Generic;
using System.Text;
namespace CabInvoiceProgram
{
    public class InvoiceGenerator
    {
        private RideRepository rideRepository;
        private readonly double MINIMUM_COST_PER_KM = 10;
        private readonly int COST_PER_TIME = 1;
        private readonly double MINIMUM_FARE = 5;
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid distance");
                }
                if (time < 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid time");
                }
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in rides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);

                }
            }
            catch (CabInvoiceException)
            {
                if (rides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "rides are null");
                }
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
    }
}
