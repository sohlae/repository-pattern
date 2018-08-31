using AutoMapper;
using RP.Business.Core;
using RP.Business.Dto;
using RP.Data.Core;
using RP.Domain;
using System.Collections.Generic;

namespace RP.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EmployeeDto AddEmployee(EmployeeDto employeeDto)
        {
            var employee = Mapper.Map<Employee>(employeeDto);

            _unitOfWork.Employees
                .Add(employee);
            _unitOfWork.Complete();

            return Mapper.Map<EmployeeDto>(employee); 
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees
                .Get(id);

            return Mapper.Map<EmployeeDto>(employee);
        }

        // Could be used for displaying all the employees to the user interface (i.e. website)
        public IEnumerable<EmployeeDto> GetEmployees()
        {
            var employees = _unitOfWork.Employees
                .GetAll();

            return Mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        // Will be used when the salary of all the employees will be computed
        public void ComputeSalaryOfAllEmployees()
        {
            var employees = _unitOfWork.Employees
                .GetAll();

            foreach(var employee in employees)
            {
                // Compute salary...
            }
        }

        public void RemoveEmployee(int id)
        {
            var employee = _unitOfWork.Employees
                .Get(id);

            _unitOfWork.Employees
                .Remove(employee);
            _unitOfWork.Complete();
        }

        public EmployeeDto EditEmployee(EmployeeDto employeeDto)
        {
            try
            {
                var employee = _unitOfWork.Employees
                    .Get(employeeDto.Id);
                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.BirthDate = employeeDto.BirthDate;

                _unitOfWork.Complete();

                return employeeDto;
            }

            catch
            {
                return null;
            }
        }
    }
}
