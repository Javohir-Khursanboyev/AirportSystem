using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Ticket;
using AirportSystem.Domain.Enums;
using Npgsql;

namespace AirportSystem.Data.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly string connectionString = Constants.ConnectionString;
    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE tickets 
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

    public async Task<IEnumerable<Tickets>> GetAllAsync()
    {
        var ticketsList = new List<Tickets>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM tickets;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Tickets ticket = new Tickets
            {
                Id = reader.GetInt64(0),
                FlightId = reader.GetInt64(1),
                TicketNumber = reader.GetInt32(2),
                TicketClass = (TicketClass)Enum.Parse(typeof(TicketClass), reader.GetString(3)),
                Price = reader.GetDecimal(4),
                IsSold = reader.GetBoolean(5),
                IsDeleted = reader.GetBoolean(9)
            };
            ticketsList.Add(ticket);
        }

        return ticketsList;
    }

    public async Task<Tickets> InsertAsync(Tickets ticket)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "INSERT INTO tickets (flight_id, ticket_number, ticket_class, price, created_at) " +
            "VALUES (@FlightId,@TicketNumber,@TicketClass,@Price,@CreatedAt) RETURNING id;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@FlightId", ticket.FlightId);
        cmd.Parameters.AddWithValue("@TicketNumber", ticket.TicketNumber);
        cmd.Parameters.AddWithValue("@TicketClass", ticket.TicketClass.ToString());
        cmd.Parameters.AddWithValue("@Price", ticket.Price);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var ticketId = await cmd.ExecuteScalarAsync();

        if (ticketId != null && ticketId != DBNull.Value)
        {
            ticket.Id = Convert.ToInt64(ticketId);
        }

        return ticket;
    }

    public async Task<Tickets> UpdateAsync(long id, Tickets ticket)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE tickets 
                       SET flight_id = @FlightId,
                           ticket_number = @TicketNumber,
                           ticket_class = @TicketClass,
                           price = @Price,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FlightId", ticket.FlightId);
                cmd.Parameters.AddWithValue("@TicketNumber", ticket.TicketNumber);
                cmd.Parameters.AddWithValue("@TicketClass", ticket.TicketClass.ToString());
                cmd.Parameters.AddWithValue("@Price", ticket.Price);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                ticket.Id = id;
                return ticket;
            }
        }
    }
}