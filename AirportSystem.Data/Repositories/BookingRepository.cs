using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Booking;
using Npgsql;

namespace AirportSystem.Data.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly string connectionString = Constants.ConnectionString;
    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE bookings 
                       SET deleted_at = @DeletedAt,
                           is_deleted = true
                       WHERE id = @Id";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@DeletedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return true;
            }
        }
    }

    public async Task<IEnumerable<Bookings>> GetAllAsync()
    {
        var bookingsList = new List<Bookings>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM bookings;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Bookings booking = new Bookings
            {
                Id = reader.GetInt64(0),
                TicketId = reader.GetInt64(1),
                CustomerId = reader.GetInt64(2),
                IsDeleted = reader.GetBoolean(6)
            };
            bookingsList.Add(booking);
        }

        return bookingsList;
    }

    public async Task<Bookings> InsertAsync(Bookings booking)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "INSERT INTO bookings (ticket_id, customer_id, created_at) " +
            "VALUES (@TicketId,@CustomerId,@CreatedAt) RETURNING id;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@TicketId", booking.TicketId);
        cmd.Parameters.AddWithValue("@CustomerId", booking.CustomerId);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var bookingId = await cmd.ExecuteScalarAsync();

        if (bookingId != null && bookingId != DBNull.Value)
        {
            booking.Id = Convert.ToInt64(bookingId);
        }

        return booking;
    }

    public async Task<Bookings> UpdateAsync(long id, Bookings booking)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE flightEmployees 
                       SET ticket_id = @TicketId,
                           customer_id = @CustomerId,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@TicketId", booking.TicketId);
                cmd.Parameters.AddWithValue("@CustomerId", booking.CustomerId);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                booking.Id = id;
                return booking;
            }
        }
    }
}
