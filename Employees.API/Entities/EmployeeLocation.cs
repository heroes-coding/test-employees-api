using System.Collections.Generic;
using System.Linq;
using System;
using Employees.API.Helpers;
using System.Globalization;

namespace Employees.API.Entities
{
  public class EmployeeLocation
  {
    public System.DateTime created { get; set; }
    public Employee employee { get; set; }
    public int id { get; set; }
    public string location { get; set; }

    public EmployeeLocation() { }

    /// <summary>
    /// This constructor takes the following string format as input:
    /// <code>"(id,created,employee(id,firstname,employeeType(id), lastname),location)"</code>
    /// I am assuming that ids are integers, and that created is a very simple
    /// 2019-01-31 style date string.
    /// </summary>
    /// <param name="inputString"></param>
    public EmployeeLocation(string inputString)
    {

      InnerAndOuterString inputWithoutParanthesis = ExtractFromParanthesis.fromDelimiter("", inputString);
      if (inputWithoutParanthesis == null)
      {
        // this would happen if there were no paranthesis or if the first character was not an opening paranthesis
        throw new ArgumentException("Employee location does not begin and end with paranthesis");
      }

      InnerAndOuterString employeeAndInputWithoutEmployee = ExtractFromParanthesis.fromDelimiter("employee", inputWithoutParanthesis.inner);

      List<string> employeeLocationParts = CSVLine.toListOfLength(employeeAndInputWithoutEmployee.outer, 4);

      this.id = GetId.getId(employeeLocationParts.ElementAt(0));

      try
      {
        this.created = DateTime.ParseExact(employeeLocationParts.ElementAt(1), "yyyy-MM-dd", CultureInfo.InvariantCulture);
      }
      catch (System.FormatException)
      {
        throw new ArgumentException("Date format not in proper format (yyyy-MM-dd)");
      }

      this.employee = new Employee(employeeAndInputWithoutEmployee.inner);
      this.location = employeeLocationParts.ElementAt(3);
    }

  }
}