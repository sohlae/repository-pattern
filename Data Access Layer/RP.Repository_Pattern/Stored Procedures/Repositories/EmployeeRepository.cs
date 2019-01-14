using RP.DataAccess.RepositoryPattern.Core;
using RP.DataAccess.RepositoryPattern.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace RP.DataAccess.RepositoryPattern.StoredProcedures.Repositories
{
    public class EmployeeRepository : IRepository<Employee>, IEmployeeRepository
    {
        private Context _context;

        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Add("sp_InsertEmployee", employee);
        }

        public void AddRange(IEnumerable<Employee> employees)
        {
            _context.AddRange("sp_SelectAllEmployees", employees);
        }

        public Employee Get(int id)
        {
            return _context.Get<Employee>("sp_SelectEmployeeById", id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.GetAll<Employee>("sp_SelectAllEmployees");
        }

        public IEnumerable<Employee> Find(Expression<Func<Employee, bool>> predicate)
        {
            throw new NotImplementedException();
        }


        public void Remove(Employee employee)
        {
            _context.Remove("sp_DeleteEmployeeById", employee);
        }

        public void RemoveRange(IEnumerable<Employee> employees)
        {
            _context.RemoveRange("sp_DeleteEmployeeById", employees);
        }

        /* 
            Other methods specific to the Employee entity will go here...
        */
        public IEnumerable<Employee> GetTop5Employees()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_context._connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("sp_SelectTop5Employees", connection) { CommandType = CommandType.StoredProcedure })
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                            });
                        }

                        return employees;
                    }
                }
            }
        }

    }
}
