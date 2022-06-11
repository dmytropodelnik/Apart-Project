using CloneBookingAPI.Services.Interfaces;

namespace CloneBookingAPI.Services.PromoCodes
{
    public class PromoCodesApplier : IApplier
    {
        public decimal? Apply(decimal price, int discount)
        {
            if (price <= 0 || discount <= 0)
            {
                return null;
            }

            price -= (price * discount) / 100;

            return price;
        }
    }
}
