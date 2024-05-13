using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SPTest.Data;
using System.Data;

namespace SPTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SPController : ControllerBase
    {

        private readonly DataContext dataContext;

        public SPController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        //[HttpPost]
        //public ActionResult Get(EmployeeSearch request)
        //{
        //    var employees = dataContext.Employees.FromSqlRaw("EXEC GetEmployeeByFirstNameAndLastName @FirstName = {0}, @LastName = {1}", request.FirstName, request.LastName).ToList();
        //    return Ok(employees);
        //}
        [HttpPost]
        public ActionResult Get(EmployeeSearch request)
        {
            var firstNameParam = new SqlParameter("@FirstName", request.FirstName);
            var lastNameParam = new SqlParameter("@LastName", request.LastName);

            var employees = dataContext.Employees
                .FromSqlRaw("EXEC GetEmployeeByFirstNameAndLastName @FirstName, @LastName", firstNameParam, lastNameParam)
                .ToList();

            return Ok(employees);
        }

        [HttpPost("GetSalary")]
        public IActionResult GetSalaryByName(string firstName, string lastName)
        {
            var salaryParam = new SqlParameter("@Salary", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };

            var parameters = new[]
            {
        new SqlParameter("@FirstName", firstName),
        new SqlParameter("@LastName", lastName),
        salaryParam
    };

            var salary = dataContext.Database
                .ExecuteSqlRaw("EXEC GetSalaryByFullName @FirstName, @LastName, @Salary OUTPUT", parameters);

            return Ok(salaryParam.Value);
        }

    }
}
