using CloneBookingAPI.Services.Interfaces;

namespace CloneBookingAPI.Services.PromoCodes
{
    public class PromoCodesApplier : IApplier
    {
        public (decimal, decimal)? Apply(decimal price, int discount)
        {
            if (price <= 0 || discount <= 0)
            {
                return null;
            }


            decimal difference = (price * discount) / 100;
            price -= difference;

            return (price, difference);
        }
    }
}
