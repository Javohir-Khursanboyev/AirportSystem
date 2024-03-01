using AirportSystem.Data.IRepositories;

namespace AirportSystem.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    //public IEnumerable<Employees> GetAllAsync()
    //{
    //    throw new NotImplementedException();
    //}

    //public Task<Employees> InsertAsync(Employees employee)
    //{
    //    using NpgsqlConnection con = new NpgsqlConnection(connectionString);
    //    await con.OpenAsync(); // Open the connection asynchronously

    //    string sql = "INSERT INTO employee (first_name) VALUES (@FirstName);";

    //    // Create a new NpgsqlCommand with the SQL command and connection
    //    using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
    //    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);

    //    // Execute the command asynchronously and retrieve the inserted ID
    //    var userId = await cmd.ExecuteScalarAsync();

    //    // If the user object has an Id property, update it with the inserted ID
    //    if (userId != null && userId != DBNull.Value)
    //    {
    //        user.Id = (int)userId;
    //    }

    //    return user;
    //}
}