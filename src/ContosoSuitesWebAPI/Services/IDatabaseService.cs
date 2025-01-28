using global::ContosoSuitesWebAPI.Entities;

namespace ContosoSuitesWebAPI.Services;

public interface IDatabaseService
{
    Task<IEnumerable<Salon>> GetSalons();
    Task<IEnumerable<Booking>> GetBookingsForSalon(int SalonId);
    Task<IEnumerable<Booking>> GetBookingsBySalonAndMinimumDate(int SalonId, DateTime dt);
    Task<IEnumerable<Booking>> GetBookingsMissingSalonRooms();
    Task<IEnumerable<Booking>> GetBookingsWithMultipleSalonRooms();

}