using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using ContosoSuitesWebAPI.Entities;
using Azure.Core;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace ContosoSuitesWebAPI.Services;

/// <summary>
/// The database service for querying the Arauco Rooms Demo database.
/// </summary>
public class DatabaseService(string connectionString) : IDatabaseService
{
    /// <summary>
    /// Get booking missing Salon rooms.
    /// </summary>
    [KernelFunction]
    [Description("Get Bookings With Missing Salon Rooms.")]
    public async Task<IEnumerable<Booking>> GetBookingsMissingSalonRooms()
    {
        var sql = """
            SELECT
                b.BookingID,
                b.CustomerID,
                b.SalonID,
                b.StayBeginDate,
                b.StayEndDate,
                b.NumberOfGuests
            FROM dbo.Booking b
            WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM dbo.BookingSalonRoom h
                    WHERE
                        b.BookingID = h.BookingID
                );
            """;
        using var conn = new SqlConnection(
            connectionString: connectionString!
        );
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var bookings = new List<Booking>();
        while (await reader.ReadAsync())
        {
            bookings.Add(new Booking
            {
                BookingID = reader.GetInt32(0),
                CustomerID = reader.GetInt32(1),
                SalonID = reader.GetInt32(2),
                StayBeginDate = reader.GetDateTime(3),
                StayEndDate = reader.GetDateTime(4),
                NumberOfGuests = reader.GetInt32(5)
            });
        }
        conn.Close();

        return bookings;
    }

    /// <summary>
    /// Get Bookings with multiple Salon rooms.
    /// </summary>
    [KernelFunction]
    [Description("Get Bookings With Multiple Salon Rooms.")]
    public async Task<IEnumerable<Booking>> GetBookingsWithMultipleSalonRooms()
    {
        var sql = """
            SELECT
                b.BookingID,
                b.CustomerID,
                b.SalonID,
                b.StayBeginDate,
                b.StayEndDate,
                b.NumberOfGuests
            FROM dbo.Booking b
            WHERE
                (
                    SELECT COUNT(1)
                    FROM dbo.BookingSalonRoom h
                    WHERE
                        b.BookingID = h.BookingID
                ) > 1;
            """;
        using var conn = new SqlConnection(
            connectionString: connectionString!
        );
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var bookings = new List<Booking>();
        while (await reader.ReadAsync())
        {
            bookings.Add(new Booking
            {
                BookingID = reader.GetInt32(0),
                CustomerID = reader.GetInt32(1),
                SalonID = reader.GetInt32(2),
                StayBeginDate = reader.GetDateTime(3),
                StayEndDate = reader.GetDateTime(4),
                NumberOfGuests = reader.GetInt32(5)
            });
        }
        conn.Close();

        return bookings;
    }

    /// <summary>
    /// Get all Salons from the database.
    /// </summary>
    [KernelFunction]
    [Description("Get all Salons.")]
    public async Task<IEnumerable<Salon>> GetSalons()
    {
        var sql = "SELECT SalonID, SalonName, City, Country FROM dbo.Salon";
        using var conn = new SqlConnection(
            connectionString: connectionString!
        );
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var Salons = new List<Salon>();
        while (await reader.ReadAsync())
        {
            Salons.Add(new Salon
            {
                SalonID = reader.GetInt32(0),
                SalonName = reader.GetString(1),
                City = reader.GetString(2),
                Country = reader.GetString(3)
            });
        }
        conn.Close();

        return Salons;
    }

    /// <summary>
    /// Get a specific Salon from the database.
    /// </summary>
    [KernelFunction]
    [Description("Get Bookings for Salon.")]
    public async Task<IEnumerable<Booking>> GetBookingsForSalon([Description("The ID of the Salon")] int SalonId)
    {
        var sql = "SELECT BookingID, CustomerID, SalonID, StayBeginDate, StayEndDate, NumberOfGuests FROM dbo.Booking WHERE SalonID = @SalonID";
        using var conn = new SqlConnection(
            connectionString: connectionString!
        );
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@SalonID", SalonId);
        using var reader = await cmd.ExecuteReaderAsync();
        var bookings = new List<Booking>();
        while (await reader.ReadAsync())
        {
            bookings.Add(new Booking
            {
                BookingID = reader.GetInt32(0),
                CustomerID = reader.GetInt32(1),
                SalonID = reader.GetInt32(2),
                StayBeginDate = reader.GetDateTime(3),
                StayEndDate = reader.GetDateTime(4),
                NumberOfGuests = reader.GetInt32(5)
            });
        }
        conn.Close();

        return bookings;
    }

    /// <summary>
    /// Get bookings for a specific Salon that are after a specified date.
    /// </summary>
    [KernelFunction]
    [Description("Get Bookings By Salon And Minimum Date.")]
    public async Task<IEnumerable<Booking>> GetBookingsBySalonAndMinimumDate([Description("The ID of the Salon")] int SalonId, [Description("The Minimum Date")] DateTime dt)
    {
        var sql = "SELECT BookingID, CustomerID, SalonID, StayBeginDate, StayEndDate, NumberOfGuests FROM dbo.Booking WHERE SalonID = @SalonID AND StayBeginDate >= @StayBeginDate";
        using var conn = new SqlConnection(
            connectionString: connectionString!
        );
        conn.Open();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@SalonID", SalonId);
        cmd.Parameters.AddWithValue("@StayBeginDate", dt);
        using var reader = await cmd.ExecuteReaderAsync();
        var bookings = new List<Booking>();
        while (await reader.ReadAsync())
        {
            bookings.Add(new Booking
            {
                BookingID = reader.GetInt32(0),
                CustomerID = reader.GetInt32(1),
                SalonID = reader.GetInt32(2),
                StayBeginDate = reader.GetDateTime(3),
                StayEndDate = reader.GetDateTime(4),
                NumberOfGuests = reader.GetInt32(5)
            });
        }
        conn.Close();

        return bookings;
    }
}
