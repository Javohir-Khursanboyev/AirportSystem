using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.FlightEmployee;
using Npgsql;

namespace AirportSystem.Data.Repositories;

public class FlightEmployeeRepository : IFlightEmployeeRepository
{
    private readonly string connectionString = Constants.ConnectionString;
    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE flightEmployees 
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

    public async Task<IEnumerable<FlightEmployees>> GetAllAsync()
    {
        var flightEmployeesList = new List<FlightEmployees>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM flightEmployees;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            FlightEmployees flightEmployee = new FlightEmployees
            {
                Id = reader.GetInt64(0),
                FlightId = reader.GetInt64(1),
                EmployeeId = reader.GetInt64(2),
                IsDeleted = reader.GetBoolean(6)
            };
            flightEmployeesList.Add(flightEmployee);
        }

        return flightEmployeesList;
    }

    public async Task<FlightEmployees> InsertAsync(FlightEmployees flightEmployee)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "INSERT INTO flightEmployees (flight_id, employee_id, created_at) " +
            "VALUES (@FlightId,@EmployeeId,@CreatedAt) RETURNING id;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@FlightId", flightEmployee.FlightId);
        cmd.Parameters.AddWithValue("@EmployeeId", flightEmployee.EmployeeId);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var flightEmployeeId = await cmd.ExecuteScalarAsync();

        if (flightEmployeeId != null && flightEmployeeId != DBNull.Value)
        {
            flightEmployee.Id = Convert.ToInt64(flightEmployeeId);
        }

        return flightEmployee;
    }

    public async Task<FlightEmployees> UpdateAsync(long id, FlightEmployees flightEmployee)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE flightEmployees 
                       SET flight_id = @FlightId,
                           employee_id = @EmployeeId,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FlightId", flightEmployee.FlightId);
                cmd.Parameters.AddWithValue("@EmployeeId", flightEmployee.EmployeeId);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                flightEmployee.Id = id;
                return flightEmployee;
            }
        }
    }
}