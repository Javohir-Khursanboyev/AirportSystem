using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Employee;
using AirportSystem.Domain.Enums;
using Npgsql;

namespace AirportSystem.Data.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string connectionString = Constants.ConnectionString;

    public async Task<Employees> InsertAsync(Employees employee)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync(); 

        string sql = "INSERT INTO employees (first_name,last_name,passport_number,phone_number,email,date_of_birth,address,employee_type,created_at) " +
            "VALUES (@FirstName,@LastName,@PassportNumber,@PhoneNumber,@Email,@DateOfBirth,@Address,@EmployeeType,@CreatedAt) RETURNING id;;";


        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
        cmd.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
        cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
        cmd.Parameters.AddWithValue("@Email", employee.Email);
        cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
        cmd.Parameters.AddWithValue("@Address", employee.Address);
        cmd.Parameters.AddWithValue("@EmployeeType", employee.EmployeeType.ToString());
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var employeeId = await cmd.ExecuteScalarAsync();

        if (employeeId != null && employeeId != DBNull.Value)
        {
            employee.Id = Convert.ToInt64(employeeId);
        }

        return employee;
    }

    public async Task<IEnumerable<Employees>> GetAllAsync()
    {
        var employeesList = new List<Employees>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM employees;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Employees employee = new Employees
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                PassportNumber = reader.GetString(3),
                PhoneNumber = reader.GetString(4),
                Email = reader.GetString(5),
                DateOfBirth = reader.GetDateTime(6),
                Address = reader.GetString(7),
                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), reader.GetString(8)),
                IsDeleted = reader.GetBoolean(12)
            };
            employeesList.Add(employee);
        }

        return employeesList;
    }

    public async Task<Employees> UpdateAsync(long id, Employees employee)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE employees 
                       SET first_name = @FirstName,
                           last_name = @LastName,
                           passport_number = @PassportNumber,
                           phone_number = @PhoneNumber,
                           email = @Email,
                           date_of_birth = @DateOfBirth,
                           address = @Address,
                           employee_type = @EmployeeType,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@PassportNumber", employee.PassportNumber);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@EmployeeType", employee.EmployeeType.ToString());
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                employee.Id = id;
                return employee;
            }
        }
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE employees 
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
}