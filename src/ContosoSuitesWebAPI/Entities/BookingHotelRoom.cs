namespace ContosoSuitesWebAPI.Entities;
public class BookingSalonRoom
{
    public required int BookingSalonRoomID { get; set; }
    public required int BookingID { get; set; }
    public required int SalonRoomID { get; set; }
}
