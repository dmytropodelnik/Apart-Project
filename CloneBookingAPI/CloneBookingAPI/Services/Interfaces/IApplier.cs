namespace CloneBookingAPI.Services.Interfaces
{
    public interface IApplier
    {
        (decimal, decimal)? Apply(decimal price, int discount);
    }
}
