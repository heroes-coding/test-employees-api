namespace Employees.API.Dtos
{
  public class EmployeeDto
  {
    public int id { get; set; }
    public string firstname { get; set; }
    public EmployeeTypeDto employeeType { get; set; }
    public string lastname { get; set; }
  }
}