
namespace TravelRepublic.Contracts.Entities
{
    public interface IHotelSearchRequestBase
    {
        string Name { get; }
        int Star { get; }
        double UserRating { get; }
        double CostMin { get; }
        double CostMax { get; }
    }
}
