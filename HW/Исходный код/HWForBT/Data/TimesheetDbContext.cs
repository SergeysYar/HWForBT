using HWForBT.Model;
using System.Data.SqlClient;

namespace HWForBT.Data
{
    public class TimesheetDbContext
    {
        private readonly string _connectionString;

        public TimesheetDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM employees", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2)
                            });
                        }
                    }
                }
            }
            return employees;
        }

        public List<Timesheet> GetTimesheets()
        {
            var timesheets = new List<Timesheet>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM timesheet", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            timesheets.Add(new Timesheet
                            {
                                Id = reader.GetInt32(0),
                                Employee = reader.GetInt32(1),
                                Reason = reader.GetInt32(2),
                                StartDate = reader.GetDateTime(3),
                                Duration = reader.GetInt32(4),
                                Discounted = reader.GetBoolean(5),
                                Description = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return timesheets;
        }

        public Timesheet GetTimesheetById(int id)
        {
            Timesheet timesheet = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM timesheet WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            timesheet = new Timesheet
                            {
                                Id = reader.GetInt32(0),
                                Employee = reader.GetInt32(1),
                                Reason = reader.GetInt32(2),
                                StartDate = reader.GetDateTime(3),
                                Duration = reader.GetInt32(4),
                                Discounted = reader.GetBoolean(5),
                                Description = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            return timesheet;
        }

        public void AddTimesheet(Timesheet timesheet)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO timesheet (employee, reason, start_date, duration, discounted, description) VALUES (@employee, @reason, @start_date, @duration, @discounted, @description)", conn))
                {
                    cmd.Parameters.AddWithValue("@employee", timesheet.Employee);
                    cmd.Parameters.AddWithValue("@reason", timesheet.Reason);
                    cmd.Parameters.AddWithValue("@start_date", timesheet.StartDate);
                    cmd.Parameters.AddWithValue("@duration", timesheet.Duration);
                    cmd.Parameters.AddWithValue("@discounted", timesheet.Discounted);
                    cmd.Parameters.AddWithValue("@description", timesheet.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTimesheet(Timesheet timesheet)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE timesheet SET employee = @employee, reason = @reason, start_date = @start_date, duration = @duration, discounted = @discounted, description = @description WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", timesheet.Id);
                    cmd.Parameters.AddWithValue("@employee", timesheet.Employee);
                    cmd.Parameters.AddWithValue("@reason", timesheet.Reason);
                    cmd.Parameters.AddWithValue("@start_date", timesheet.StartDate);
                    cmd.Parameters.AddWithValue("@duration", timesheet.Duration);
                    cmd.Parameters.AddWithValue("@discounted", timesheet.Discounted);
                    cmd.Parameters.AddWithValue("@description", timesheet.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTimesheet(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM timesheet WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
