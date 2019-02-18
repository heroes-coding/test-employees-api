using System;
using Xunit;
using Employees.API.Entities;

public class EmployeeTests
{
  const int id = 2;
  const int employeeId = 5;
  const int employeeTypeId = 3;
  const string correctDate = "2020-02-20";
  const string incorrectDate = "02-20-2020";
  const string firstName = "Jeremy";
  static string employeeType = String.Format("employeeType({0})", employeeTypeId);
  const string lastName = "Schutte";
  static string correctEmployee = String.Format("employee({0},{1},{2},{3})", employeeId, firstName, employeeType, lastName);
  const string location = "Bolivar";

  // Remember the standard input format:
  // "(id,created,employee(id,firstname,employeeType(id), lastname),location)"
  [Fact]
  public void EmployeeLocationHasRightAttributes()
  {
    string input = String.Format("({0},{1},{2},{3})", id, correctDate, correctEmployee, location);
    EmployeeLocation employeeLocation = new EmployeeLocation(input);
    Assert.Equal(employeeId, employeeLocation.employee.id);
    Assert.Equal(firstName, employeeLocation.employee.firstname);
    Assert.Equal(lastName, employeeLocation.employee.lastname);
    Assert.Equal(employeeTypeId, employeeLocation.employee.employeeType.id);
  }

  [Fact]
  public void EmployeeLocationAttributesMustBeInCorrectOrder()
  {
    string input = String.Format("({3},{1},{0},{2})", id, correctDate, correctEmployee, location);
    var exception = Record.Exception(() => new EmployeeLocation(input));
    Assert.IsType<ArgumentException>(exception);
  }

  [Fact]
  public void EmployeeLocationMustStartAndEndWithParanthesis()
  {
    string[] employeeLocationTypos = {
      String.Format("{0},{1},{2},{3})", id, correctDate, correctEmployee, location),
      String.Format("({0},{1},{2},{3}", id, correctDate, correctEmployee, location),
      String.Format("{0},{1},{2},{3}", id, correctDate, correctEmployee, location)
    };

    foreach (string input in employeeLocationTypos)
    {
      var exception = Record.Exception(() => new EmployeeLocation(input));
      Assert.IsType<ArgumentException>(exception);
    }
  }

  [Fact]
  public void EmployeeLocationMustHaveExactlyFourCommaSeparatedValues()
  {
    string longInput = String.Format("({0},{1},{2},{3},extra)", id, correctDate, correctEmployee, location);
    var longException = Record.Exception(() => new EmployeeLocation(longInput));
    string shortInput = String.Format("({0},{1},{2})", id, correctDate, correctEmployee);
    var shortException = Record.Exception(() => new EmployeeLocation(shortInput));
    Assert.IsType<ArgumentException>(longException);
    Assert.IsType<ArgumentException>(shortException);
  }

  [Fact]
  public void EmployeeLocationCannotHaveWrongDateFormat()
  {
    string input = String.Format("({0},{1},{2},{3})", id, incorrectDate, correctEmployee, location);
    var exception = Record.Exception(() => new Employee(input));
    Assert.IsType<ArgumentException>(exception);
  }

  [Fact]
  public void EmployeeLocationMustHaveIntegerId()
  {
    string input = String.Format("({0},{1},{2},{3})", "bob", correctDate, correctEmployee, location);
    var exception = Record.Exception(() => new Employee(input));
    Assert.IsType<ArgumentException>(exception);
  }

  [Fact]
  public void EmployeeHasRightAttributes()
  {
    string input = String.Format("{0},{1},{2},{3}", employeeId, firstName, employeeType, lastName);
    Employee employee = new Employee(input);
    Assert.Equal(employeeId, employee.id);
    Assert.Equal(firstName, employee.firstname);
    Assert.Equal(lastName, employee.lastname);
    Assert.Equal(employee.employeeType.id, employeeTypeId);
  }

  [Fact]
  public void EmployeeMustHaveIntegerId()
  {
    string input = String.Format("NaN,{0},{1},{2}", firstName, employeeType, lastName);
    var exception = Record.Exception(() => new Employee(input));
    Assert.IsType<ArgumentException>(exception);
  }

  [Fact]
  public void EmployeeTypeMustBeInProperFormat()
  {
    string[] employeeTypos = { "employeeTypo(3)", "employeeType(Bob)", "employeeType(3", "employeeType3)", "(employeeType(3))" };
    foreach (string employeeType in employeeTypos)
    {
      string input = String.Format("{0},{1},{2},{3}", id, firstName, "employeeTypo(3)", lastName);
      var exception = Record.Exception(() => new Employee(input));
      Assert.IsType<ArgumentException>(exception);
    }
  }

  [Fact]
  public void EmployeeMustHaveExactlyFourCommaSeparatedValues()
  {
    string longInput = String.Format("{0},{1},{2},{3},extra", id, firstName, employeeType, lastName);
    var longException = Record.Exception(() => new Employee(longInput));
    string shortInput = String.Format("{0},{1},{2}", id, firstName, employeeType, lastName);
    var shortException = Record.Exception(() => new Employee(shortInput));
    Assert.IsType<ArgumentException>(longException);
    Assert.IsType<ArgumentException>(shortException);
  }



}

