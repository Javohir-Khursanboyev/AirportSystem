using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Entities.Flight;
using AirportSystem.Domain.Enums;
using Npgsql;

namespace AirportSystem.Data.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly string connectionString = Constants.ConnectionString;
    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE flights 
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

    public async Task<IEnumerable<Flights>> GetAllAsync()
    {
        var flightsList = new List<Flights>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM flights;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Flights flight = new Flights
            {
                Id = reader.GetInt64(0),
                AircraftId = reader.GetInt64(1),
                PlaceOfDeparture = reader.GetString(2),
                PlaceOfArrival = reader.GetString(3),
                DepartureTime = reader.GetDateTime(4),
                ArrivalTime = reader.GetDateTime(5),
                FlightStatus = (FlightStatus)Enum.Parse(typeof(FlightStatus), reader.GetString(6)),
                IsDeleted = reader.GetBoolean(10)
            };
            flightsList.Add(flight);
        }

        return flightsList;
    }

    public async Task<Flights> InsertAsync(Flights flight)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "INSERT INTO flights (aircraft_id, place_of_departure, place_of_arrival, departure_time, arrival_time, flight_status, created_at) " +
            "VALUES (@AircraftId,@PlaceOfDeparture,@PlaceOfArrival,@DepartureTime,@ArrivalTime,@FlightStatus,@CreatedAt) RETURNING id;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@AircraftId", flight.AircraftId);
        cmd.Parameters.AddWithValue("@PlaceOfDeparture", flight.PlaceOfDeparture);
        cmd.Parameters.AddWithValue("@PlaceOfArrival", flight.PlaceOfArrival);
        cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
        cmd.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
        cmd.Parameters.AddWithValue("@FlightStatus", flight.FlightStatus.ToString());
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var flightId = await cmd.ExecuteScalarAsync();

        if (flightId != null && flightId != DBNull.Value)
        {
            flight.Id = Convert.ToInt64(flightId);
        }

        return flight;
    }

    public async Task<Flights> UpdateAsync(long id, Flights flight)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE flights 
                       SET aircraft_id = @AircraftId,
                           place_of_departure = @PlaceOfDeparture,
                           place_of_arrival = @PlaceOfArrival,
                           departure_time = @DepartureTime,
                           arrival_time = @ArrivalTime,
                           flight_status = @FlightStatus,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@AircraftId", flight.AircraftId);
                cmd.Parameters.AddWithValue("@PlaceOfDeparture", flight.PlaceOfDeparture);
                cmd.Parameters.AddWithValue("@PlaceOfArrival", flight.PlaceOfArrival);
                cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
                cmd.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
                cmd.Parameters.AddWithValue("@FlightStatus", flight.FlightStatus.ToString());
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                flight.Id = id;
                return flight;
            }
        }
    }
}
