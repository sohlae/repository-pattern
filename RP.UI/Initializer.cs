using RP.Business.Core;
using RP.Business.Dto;
using System;

namespace RP.UI
{
    public class Initializer
    {
        private readonly IEmployeeBusiness _employeeBusiness;

        public Initializer(IEmployeeBusiness employeeBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }

        public void Run()
        {
            #region Adding an Employee
            //var employee = _employeeBusiness.AddEmployee(new EmployeeDto
            //{
            //    FirstName = "Dayle",
            //    LastName = "Sacopanio",
            //    BirthDate = new DateTime(2000, 10, 01)
            //});

            //Console.WriteLine("{0} {1} with birthday on {2} has been added to the database using Entity Framework.",
            //    employee.FirstName,
            //    employee.LastName,
            //    employee.BirthDate.Date);

            //var employee = _employeeBusiness.AddEmployee(new EmployeeDto
            //{
            //    FirstName = "Roylan",
            //    LastName = "Madronero",
            //    BirthDate = new DateTime(2000, 10, 01)
            //});

            //Console.WriteLine("{0} {1} with birthday on {2} has been added to the database using Stored Procedures.",
            //    employee.FirstName,
            //    employee.LastName,
            //    employee.BirthDate.Date);
            #endregion

            #region Getting all Employees
            //var employees = _employeeBusiness.GetEmployees();

            //foreach (var employee in employees)
            //{
            //    Console.WriteLine("{0} {1} with birthday on {2} has been extracted from the database.",
            //        employee.FirstName,
            //        employee.LastName,
            //        employee.BirthDate.Date);
            //}
            #endregion

            #region Getting a specific Employee
            //var employee = _employeeBusiness.GetEmployeeById(1);

            //Console.WriteLine("{0} {1} with birthday on {2} has been extracted from the database.",
            //    employee.FirstName,
            //    employee.LastName,
            //    employee.BirthDate.Date);
            #endregion

            #region Removing an Employee
            //_employeeBusiness.RemoveEmployee(10);

            //Console.WriteLine("Employee 10 has been removed from the database.");
            #endregion

            #region Updating an Employee
            //var employeeDto = new EmployeeDto
            //{
            //    Id = 11,
            //    FirstName = "Dave",
            //    LastName = "Motea"
            //};

            //employeeDto = _employeeBusiness.EditEmployee(employeeDto);

            //Console.WriteLine("Employee {0} with birthday on {1} has been modifed from the database.",
            //    employeeDto.Id,
            //    employeeDto.BirthDate.Date);
            #endregion

            Console.ReadLine();
        }
    }
}
