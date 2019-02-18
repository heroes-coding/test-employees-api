using System;
using Employees.API.Helpers;

namespace Employees.API.Entities
{
  public class EmployeeType
  {
    public int id { get; set; }
    /// <summary>
    /// This constructor takes the following string format as input:
    /// <code>"id"</code>
    /// </summary>
    /// <param name="inputString"></param>
    public EmployeeType(string input)
    {
      this.id = GetId.getId(input);
    }
  }
}