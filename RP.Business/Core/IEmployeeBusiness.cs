using RP.Business.Dto;
using System.Collections.Generic;

namespace RP.Business.Core
{
    public interface IEmployeeBusiness
    {
        EmployeeDto AddEmployee(EmployeeDto employeeDto);
        EmployeeDto EditEmployee(EmployeeDto employeeDto);
        EmployeeDto GetEmployeeById(int id);
        IEnumerable<EmployeeDto> GetEmployees();
        void RemoveEmployee(int id);

        void ComputeSalaryOfAllEmployees();
    }
}
