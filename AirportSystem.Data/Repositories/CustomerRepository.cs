using AirportSystem.Data.IRepositories;
using AirportSystem.Domain.Configurations;
using AirportSystem.Domain.Entities.Customer;
using Npgsql;

namespace AirportSystem.Data.Repositories;
public class CustomerRepository : ICustomerRepository
{
    private readonly string connectionString = Constants.ConnectionString;

    public async Task<bool> DeleteAsync(long id)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE customers 
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

    public async Task<IEnumerable<Customers>> GetAllAsync()
    {
        var customersList = new List<Customers>();

        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "SELECT * FROM customers;";

        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            Customers customer = new Customers()
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                PassportNumber = reader.GetString(3),
                Email = reader.GetString(4),
                PhoneNumber = reader.GetString(5),
                DateOfBirth = reader.GetDateTime(6),
                Balance = reader.GetDecimal(7),
                IsDeleted = reader.GetBoolean(11)
            };
            customersList.Add(customer);
        }

        return customersList;
    }

    public async Task<Customers> InsertAsync(Customers customer)
    {
        using NpgsqlConnection con = new NpgsqlConnection(connectionString);
        await con.OpenAsync();

        string sql = "INSERT INTO customers (first_name,last_name,passport_number,phone_number,email,date_of_birth,balance,created_at) " +
            "VALUES (@FirstName,@LastName,@PassportNumber,@PhoneNumber,@Email,@DateOfBirth,@Balance,@CreatedAt) RETURNING id;";


        using NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
        cmd.Parameters.AddWithValue("@LastName", customer.LastName);
        cmd.Parameters.AddWithValue("@PassportNumber", customer.PassportNumber);
        cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
        cmd.Parameters.AddWithValue("@Email", customer.Email);
        cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
        cmd.Parameters.AddWithValue("@Balance", customer.Balance);
        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.UtcNow);

        var employeeId = await cmd.ExecuteScalarAsync();

        if (employeeId != null && employeeId != DBNull.Value)
        {
            customer.Id = Convert.ToInt64(employeeId);
        }

        return  customer;
    }

    public async Task<Customers> UpdateAsync(long id, Customers customer)
    {
        using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
        {
            await con.OpenAsync();

            string sql = @"UPDATE customers 
                       SET first_name = @FirstName,
                           last_name = @LastName,
                           passport_number = @PassportNumber,
                           phone_number = @PhoneNumber,
                           email = @Email,
                           date_of_birth = @DateOfBirth,
                           balance = @Balance,
                           updated_at = @UpdatedAt,
                           is_deleted = @IsDeleted
                       WHERE id = @Id ;";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@PassportNumber", customer.PassportNumber);
                cmd.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                cmd.Parameters.AddWithValue("@Balance", customer.Balance);
                cmd.Parameters.AddWithValue("@UpdatedAt", DateTime.UtcNow);
                cmd.Parameters.AddWithValue("@IsDeleted", false);
                cmd.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                customer.Id = id;
                return customer;
            }
        }
    }
}
