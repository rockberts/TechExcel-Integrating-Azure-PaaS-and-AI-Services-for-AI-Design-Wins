namespace ContosoSuitesWebAPI.Entities;
public class SalonRoom
{
    public required int SalonRoomID { get; set; }
    public required int SalonID { get; set; }
    public required string RoomNumber { get; set; }
    public required int SalonRoomTypeID { get; set; }
}
