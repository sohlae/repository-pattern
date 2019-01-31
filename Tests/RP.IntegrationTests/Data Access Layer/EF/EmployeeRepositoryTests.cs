using NUnit.Framework;
using RP.DataAccess.RepositoryPattern.EF;
using RP.DataAccess.RepositoryPattern.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RP.IntegrationTests.DataAccessLayer.EF
{
    [SetUpFixture]
    public class IntegrationTestSetup
    {
        private UnitOfWork _unitOfWork;
        private Employee _employee;

        [OneTimeSetUp]
        public void SetUp()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Integration-0", LastName = "Integration-0" },
                new Employee { FirstName = "Integration-1", LastName = "Integration-1" },
                new Employee { FirstName = "Integration-2", LastName = "Integration-2" }
            };

            _unitOfWork = new UnitOfWork(new RPContext());
            _unitOfWork.Employees.AddRange(employees);
            _unitOfWork.Complete();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Integration-0", LastName = "Integration-0" },
                new Employee { FirstName = "Integration-1", LastName = "Integration-1" },
                new Employee { FirstName = "Integration-2", LastName = "Integration-2" },
                new Employee { FirstName = "Integration-3", LastName = "Integration-3" },
                new Employee { FirstName = "Integration-4", LastName = "Integration-4" },
                new Employee { FirstName = "Integration-5", LastName = "Integration-5" },
                new Employee { FirstName = "Integration-6", LastName = "Integration-6" },
                new Employee { FirstName = "Integration-7", LastName = "Integration-7" }
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
                FirstName = "Integration-3",
                LastName = "Integration-3"
            };

            _unitOfWork.Employees.Add(_employee);
            _unitOfWork.Complete();

            Assert.That(_employee.Id, Is.Not.EqualTo(0));
        }

        [Test]
        public void AddRange_WhenCalled_EmployeesWithoutIdShouldBeZero()
        {
            var employees = new List<Employee>
            {
                new Employee { FirstName = "Integration-4", LastName = "Integration-4" },
                new Employee { FirstName = "Integration-5", LastName = "Integration-5" }
            };

            _unitOfWork.Employees.AddRange(employees);
            _unitOfWork.Complete();
            var result = employees.Where(e => e.Id == 0);

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Find_WhenCalled_CountShouldBeOne()
        {
            var result = _unitOfWork.Employees
                .Find(e => e.FirstName == "Integration-0");

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Find_WhenCalled_ReturnTypeShouldBeInstanceOfIEnumberableOfEmployee()
        {
            var result = _unitOfWork.Employees
                .Find(e => e.FirstName == "Integration-0");

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<Employee>)));
        }

        [Test]
        public void Get_WhenCalled_FirstNameShouldBeIntegration6()
        {
            _employee = new Employee { FirstName = "Integration-6", LastName = "Integration-6" };
            _unitOfWork.Employees.Add(_employee);
            _unitOfWork.Complete();

            var result = _unitOfWork.Employees
                .Get(_employee.Id);

            Assert.That(result.FirstName, Is.EqualTo("Integration-6"));
        }

        [Test]
        public void Get_WhenCalled_ReturnTypeShouldBeInstanceOfEmployee()
        {
            _employee = new Employee { FirstName = "Integration-7", LastName = "Integration-7" };
            _unitOfWork.Employees.Add(_employee);
            _unitOfWork.Complete();

            var result = _unitOfWork.Employees
                .Get(_employee.Id);

            Assert.That(result, Is.InstanceOf((typeof(Employee))));
        }

        [Test]
        public void GetAll_WhenCalled_ResultCountShouldNotBeZero()
        {
            var result = _unitOfWork.Employees
                .GetAll();

            Assert.That(result.Count(), Is.Not.EqualTo(0));
        }

        [Test]
        public void GetAll_WhenCalled_ReturnTypeShouldBeInstanceOfIEnumberableOfEmployee()
        {
            var result = _unitOfWork.Employees
                .GetAll();

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<Employee>)));
        }

        [Test]
        public void Remove_WhenCalled_ResultCountShouldBeZero()
        {
            _employee = _unitOfWork.Employees
               .Find(e => e.FirstName == "Integration-0")
               .FirstOrDefault();

            _unitOfWork.Employees.Remove(_employee);
            _unitOfWork.Complete();
            var result = _unitOfWork.Employees
               .Find(e => e.FirstName == "Integration-0");

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void RemoveRange_WhenCalled_ResultCountShouldBeZero()
        {
            var employees = new List<Employee>();

            _employee = _unitOfWork.Employees
               .Find(e => e.FirstName == "Integration-1")
               .FirstOrDefault();
            employees.Add(_employee);

            _employee = _unitOfWork.Employees
                .Find(e => e.FirstName == "Integration-2")
                .FirstOrDefault();
            employees.Add(_employee);

            _unitOfWork.Employees.RemoveRange(employees);
            _unitOfWork.Complete();

            var result = _unitOfWork.Employees
                .Find(e => e.FirstName == "Integration-1" || e.FirstName == "Integration-2");

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetTop5Employees_WhenCalled_ResultCountShouldBeLessThanOrEqualToFive()
        {
            var result = _unitOfWork.Employees
                .GetTop5Employees();

            Assert.That(result.Count(), Is.LessThanOrEqualTo(5));
        }

        [Test]
        public void GetTop5Employees_WhenCalled_ResultTypeShouldBeInstanceOfIEnumerableOfEmployee()
        {
            var result = _unitOfWork.Employees
                .GetTop5Employees();

            Assert.That(result, Is.InstanceOf(typeof(IEnumerable<Employee>)));
        }
    }
}
