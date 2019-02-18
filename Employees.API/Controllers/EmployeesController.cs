using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Employees.API.Entities;
using Employees.API.Dtos;
using AutoMapper;

namespace Employees.API.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class EmployeesController : ControllerBase
  {

    private IMapper _mapper;
    public EmployeesController(IMapper mapper)
    {
      this._mapper = mapper;
    }

    // Get api/employees/location/
    [HttpGet("location/{input}")]
    public IActionResult GetEmployeeLocation(string input)
    {
      EmployeeLocation employeeLocation = new EmployeeLocation(input);
      return Ok(_mapper.Map<EmployeeLocationDto>(employeeLocation));

    }

  }
}
