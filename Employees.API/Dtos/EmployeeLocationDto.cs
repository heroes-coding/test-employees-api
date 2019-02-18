
namespace Employees.API.Dtos
{
  public class EmployeeLocationDto
  {
    public System.DateTime created { get; set; }
    public EmployeeDto employee { get; set; }
    public int id { get; set; }
    public string location { get; set; }

  }
}