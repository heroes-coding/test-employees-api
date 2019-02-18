using System.Collections.Generic;
using System.Linq;
using System;
using Employees.API.Helpers;

namespace Employees.API.Entities

{
  public class Employee
  {
    public int id { get; set; }
    public string firstname { get; set; }
    public EmployeeType employeeType { get; set; }
    public string lastname { get; set; }
    /// <summary>
    /// This constructor takes the following string format as input:
    /// <code>"id,firstname,employeeType(id),lastname"</code>
    /// </summary>
    /// <param name="inputString"></param>
    public Employee(string inputString)
    {
      InnerAndOuterString employeeTypeAndEmployeeWithoutET = ExtractFromParanthesis.fromDelimiter("employeeType", inputString);
      if (employeeTypeAndEmployeeWithoutET == null)
      {
        throw new ArgumentException("employeeType missing or missing its following paranthesis");
      }

      List<string> employeeParts = CSVLine.toListOfLength(employeeTypeAndEmployeeWithoutET.outer, 4);
      this.id = GetId.getId(employeeParts.ElementAt(0));
      this.firstname = employeeParts.ElementAt(1);
      this.lastname = employeeParts.ElementAt(3);
      this.employeeType = new EmployeeType(employeeTypeAndEmployeeWithoutET.inner);


    }


  }
}