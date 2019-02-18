using System;

namespace Employees.API.Helpers
{
  public class GetId
  {
    /// <summary>
    /// Returns a parsed id or throws.  I created this because it was used three times, even though it is pretty simple.
    /// </summary>
    /// <param name="stringId"></param>
    /// <returns></returns>
    public static int getId(string stringId)
    {
      if (int.TryParse(stringId, out int id))
      {
        return id;
      }
      else
      {
        throw new ArgumentException("Id must be parsable to an integer");
      }
    }
  }
}