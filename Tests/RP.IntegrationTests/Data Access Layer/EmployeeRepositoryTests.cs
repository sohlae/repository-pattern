using NUnit.Framework;
using RP.DataAccess.RepositoryPattern.EF;
using RP.DataAccess.RepositoryPattern.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RP.IntegrationTests.DataAccessLayer
{
    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private UnitOfWork _unitOfWork;
        private Employee _employee;

        [OneTimeSetUp]
        public void SetUp()
        {
            _unitOfWork = new UnitOfWork(new RPContext());
            _unitOfWork.Employees.Add(new Employee { FirstName = "Integration-0", LastName = "Integration-0" });
            _unitOfWork.Complete();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Integration-0", LastName = "Integration-0" },
                new Employee { FirstName = "Integration-1", LastName = "Integration-1" }
            };

            _unitOfWork = new UnitOfWork(new RPContext());

            foreach (var employee in employees)
            {
                _employee = _unitOfWork.Employees
                    .Find(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName)
                    .SingleOrDefault();

                if (_employee != null)
                {
                    _unitOfWork.Employees.Remove(_employee);
                    _unitOfWork.Complete();
                }
            }
        }
    }

    [TestFixture]
    public class EmployeeRepositoryTests
    {
        private UnitOfWork _unitOfWork;
        private Employee _employee;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new UnitOfWork(new RPContext());
        }

        [Test]
        public void Add_WhenCalled_EmployeeIdShouldNotBeZero()
        {
            _employee = new Employee
            {
                FirstName = "Integration-1",
                LastName = "Integration-1"
            };

            _unitOfWork.Employees.Add(_employee);
            _unitOfWork.Complete();

            Assert.That(_employee.Id, Is.Not.EqualTo(0));
        }
    }
}
