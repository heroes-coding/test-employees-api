using System.Collections.Generic;
using System.Linq;
using System;

namespace Employees.API.Helpers
{
  public static class CSVLine
  {
    /// <summary>
    /// Takes as input a comma separated value string and returns it as a list of the values, so long as the expected length is what is passed in.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expectedLength"></param>
    /// <returns></returns>
    public static List<string> toListOfLength(string input, int expectedLength)
    {
      List<string> inputParts = input.Split(",").Select(p => p.Trim()).ToList();
      if (inputParts.Count < expectedLength || inputParts.Count > expectedLength)
      {
        throw new ArgumentException(String.Format("The following input should have {0} comma separated values, instead it has {1}:\n{2}", expectedLength, inputParts.Count, input));
      }
      return inputParts;
    }
  }
}