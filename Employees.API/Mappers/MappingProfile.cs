using AutoMapper;
using Employees.API.Dtos;
using Employees.API.Entities;

namespace Employees.API.Mappers
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Employee, EmployeeDto>();
      CreateMap<EmployeeLocation, EmployeeLocationDto>();
      CreateMap<EmployeeType, EmployeeTypeDto>();
    }
  }
}