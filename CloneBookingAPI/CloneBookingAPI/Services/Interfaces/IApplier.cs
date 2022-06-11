namespace CloneBookingAPI.Services.Interfaces
{
    public interface IApplier
    {
        decimal? Apply(decimal price, int discount);
    }
}
