using AutoMapper;
using Moq;
using NUnit.Framework;
using RP.Business;
using RP.Business.Dto;
using RP.Business.Profiles;
using RP.DataAccess.RepositoryPattern.Core;
using RP.DataAccess.RepositoryPattern.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RP.UnitTests
{
    [TestFixture]
    public class EmployeeBusinessTests
    {
        private Employee _employee;
        private EmployeeDto _dto;
        private EmployeeBusiness _business;

        private Mock<IUnitOfWork> _unitOfWork;

        public EmployeeBusinessTests()
        {
            Mapper.Initialize(cfg => { cfg.AddProfile<EmployeeProfile>(); });
        }

        [SetUp]
        public void SetUp()
        {
            _employee = new Employee { FirstName = "John" };
            _dto = new EmployeeDto { FirstName = "John" };

            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [Test]
        public void AddEmployee_EmployeeIsNotNull_AddEmployeeToDatabase()
        {
            _unitOfWork.Setup(uow => uow.Employees.Add(_employee));
            _business = new EmployeeBusiness(_unitOfWork.Object);

            _business.AddEmployee(_dto);

            _unitOfWork.Verify(uow => 
                uow.Employees.Add(It.Is<Employee>(e => e.FirstName == "John")), 
                Times.Once);
        }

        [Test]
        public void AddEmployee_EmployeeIsNotNull_EmployeeFirstNameShouldBeJohn()
        {
            _unitOfWork.Setup(uow => uow.Employees.Add(_employee));
            _business = new EmployeeBusiness(_unitOfWork.Object);

            var result = _business.AddEmployee(_dto);

            Assert.That(result.FirstName, Is.EqualTo("John"));
        }

        [Test]
        public void AddEmployee_EmployeeIsNull_ThrowArgumentNullException()
        {
            _business = new EmployeeBusiness(_unitOfWork.Object);

            Assert.That(() => _business.AddEmployee(null), Throws.ArgumentNullException);
        }

        [Test]
        public void GetEmployeeById_EmployeeDoesNotExist_ReturnsNull()
        {
            _unitOfWork.Setup(uow => uow.Employees.Get(1))
                .Returns((Employee)null);
            _business = new EmployeeBusiness(_unitOfWork.Object);

            var result = _business.GetEmployeeById(1);

            Assert.IsNull(result);
        }

        [Test]
        public void GetEmployeeById_EmployeeExists_ReturnsEmployeeWithFirstNameOfJohn()
        {
            _unitOfWork.Setup(uow => uow.Employees.Get(1))
                .Returns(_employee);
            _business = new EmployeeBusiness(_unitOfWork.Object);

            var result = _business.GetEmployeeById(1);

            Assert.That(result.FirstName, Is.EqualTo("John"));
        }

        [Test]
        public void GetEmployees_WhenCalled_ShouldReturnIEnumerableOfEmployeeDto()
        {
            _unitOfWork.Setup(uow => uow.Employees.GetAll())
                .Returns(new List<Employee> { _employee });
            _business = new EmployeeBusiness(_unitOfWork.Object);

            var result = _business.GetEmployees();

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<EmployeeDto>)));
        }

        [Test]
        public void GetEmployees_ResultCountIsOne_ShouldReturnListWithOneItem()
        {
            _unitOfWork.Setup(uow => uow.Employees.GetAll())
                .Returns(new List<Employee> { _employee });
            _business = new EmployeeBusiness(_unitOfWork.Object);

            var result = _business.GetEmployees();

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetEmployees_ResultCountIsZero_ShouldReturnNull()
        {
            _unitOfWork.Setup(uow => uow.Employees.GetAll())
                .Returns(new List<Employee>());
            _business = new EmployeeBusiness(_unitOfWork.Object);

            var result = _business.GetEmployees();

            Assert.Null(result);
        }

        [Test]
        public void RemoveEmployee_EmployeeIsNotNull_RemoveEmployeeFromTheDatabase()
        {
            _unitOfWork.Setup(uow => uow.Employees.Get(1))
                .Returns(_employee);
            _unitOfWork.Setup(uow => uow.Employees.Remove(_employee));
            _business = new EmployeeBusiness(_unitOfWork.Object);

            _business.RemoveEmployee(1);

            _unitOfWork.Verify(uow =>
                uow.Employees.Remove(It.Is<Employee>(e => e.FirstName == "John")),
                Times.Once);
        }

        [Test]
        public void RemoveEmployee_EmployeeIsNull_ThrowNullReferenceException()
        {
            _unitOfWork.Setup(uow => uow.Employees.Get(1))
               .Returns((Employee)null);
            _business = new EmployeeBusiness(_unitOfWork.Object);

            Assert.That(() => _business.RemoveEmployee(1), Throws.InstanceOf<NullReferenceException>());
        }
    }
}
