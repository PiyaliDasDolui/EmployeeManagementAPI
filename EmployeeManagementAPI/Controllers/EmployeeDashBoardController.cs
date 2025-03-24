using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeDashBoardController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public EmployeeDashBoardController(IWebHostEnvironment env)
        {
            _env = env;   
        }
        [HttpGet(Name = "GetAllEmployee")]
        public async Task<IActionResult>  GetAllEmployee()
        {
            var filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "employeedata.json");
            if (!System.IO.File.Exists(filePath))
                return NotFound("Not found file path");

            var json = await System.IO.File.ReadAllTextAsync(filePath);

            var employees = JsonSerializer.Deserialize<List<employeeData>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Ok(employees);
        }
    }
}
