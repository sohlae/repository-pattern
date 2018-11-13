using AutoMapper;
using Moq;
using NUnit.Framework;
using RP.Business;
using RP.Business.Dto;
using RP.Business.Profiles;
using RP.Data.Core;
using RP.Domain;
using System;

namespace RP.UnitTests
{
    [TestFixture]
    public class EmployeeBusinessTests
    {
        private Employee _employee;
        private EmployeeDto _employeeDto;

        private Mock<IUnitOfWork> _unitOfWork;
        private EmployeeBusiness _employeeBusiness;

        public EmployeeBusinessTests()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<EmployeeProfile>(); });
        }

        [SetUp]
        public void SetUp()
        {
            _employee = new Employee
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                BirthDate = new DateTime(1965, 12, 31)
            };

            _employeeDto = new EmployeeDto
            {
                FirstName = "John",
                LastName = "Smith",
                BirthDate = new DateTime(1965, 12, 31)
            };

            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public void AddEmployee_EmployeeIsNotNull_AddEmployeeToDatabase()
        {
            _unitOfWork.Setup(uow => uow.Employees.Add(new Employee()));
            _employeeBusiness = new EmployeeBusiness(_unitOfWork.Object);

            _employeeBusiness.AddEmployee(_employeeDto);

            _unitOfWork.Verify(uow => uow.Employees.Add(It.IsAny<Employee>()), Times.Once);
        }

        [Test]
        public void AddEmployee_EmployeeIsNotNull_ReturnEmployeeObject()
        {
            _unitOfWork.Setup(uow => uow.Employees.Add(new Employee()));
            _employeeBusiness = new EmployeeBusiness(_unitOfWork.Object);

            var result = _employeeBusiness.AddEmployee(_employeeDto);

            Assert.That(result, Is.TypeOf<EmployeeDto>());
        }

        [Test]
        public void AddEmployee_EmployeeIsNull_DoNotAddAnythingToDatabase()
        {
            _employeeBusiness = new EmployeeBusiness(_unitOfWork.Object);

            _employeeBusiness.AddEmployee(null);

            _unitOfWork.Verify(uow => uow.Employees.Add(It.IsAny<Employee>()), Times.Never);
        }

        [Test]
        public void AddEmployee_EmployeeIsNull_ReturnNull()
        {
            _employeeBusiness = new EmployeeBusiness(_unitOfWork.Object);

            var result = _employeeBusiness.AddEmployee(null);

            Assert.IsNull(result);
        }

        [Test]
        public void GetEmployeeById_EmployeeDoesNotExist_ReturnsNull()
        {
            _unitOfWork.Setup(uow => uow.Employees.Get(1))
                .Returns((Employee)null);
            _employeeBusiness = new EmployeeBusiness(_unitOfWork.Object);

            var result = _employeeBusiness.GetEmployeeById(1);

            Assert.IsNull(result);
        }

        [Test]
        public void GetEmployeeById_EmployeeExists_ReturnsEmployeeAsDto()
        {
            _unitOfWork.Setup(uow => uow.Employees.Get(1))
                .Returns(_employee);
            _employeeBusiness = new EmployeeBusiness(_unitOfWork.Object);

            var result = _employeeBusiness.GetEmployeeById(1);

            Assert.That(result.Id, Is.EqualTo(_employee.Id));
        }
    }
}
