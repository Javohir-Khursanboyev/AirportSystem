using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Aircraft;
using Npgsql;

namespace AirportSystem.Data.Repositories;

public class AircraftRepository : IAircraftRepository
{
    private readonly string connectionString = Constants.ConnectionString;
    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE aircrafts 
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

    public async Task<IEnumerable<Aircrafts>> GetAllAsync()
    {
        var aircraftsList = new List<Aircrafts>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM aircrafts;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Aircrafts aircraft = new Aircrafts()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                TotalNumberOfSeats = reader.GetInt32(2),
                IsDeleted = reader.GetBoolean(6)
            };
            aircraftsList.Add(aircraft);
        }

        return aircraftsList;
    }

    public async Task<Aircrafts> InsertAsync(Aircrafts aircraft)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "INSERT INTO aircrafts (name,total_number_of_seats,created_at) " +
            "VALUES (@FirstName,@TotalNumberOfSeats,@CreatedAt) RETURNING id;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@FirstName", aircraft.Name);
        cmd.Parameters.AddWithValue("@TotalNumberOfSeats", aircraft.TotalNumberOfSeats);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var aircraftId = await cmd.ExecuteScalarAsync();

        if (aircraftId != null && aircraftId != DBNull.Value)
        {
            aircraft.Id = Convert.ToInt64(aircraftId);
        }

        return aircraft;
    }

    public async Task<Aircrafts> UpdateAsync(long id, Aircrafts aircraft)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE aircrafts 
                       SET name = @Name,
                           total_number_of_seats = @TotalNumberOfSeats,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Name", aircraft.Name);
                cmd.Parameters.AddWithValue("@TotalNumberOfSeats", aircraft.TotalNumberOfSeats);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                aircraft.Id = id;
                return aircraft;
            }
        }
    }
}
