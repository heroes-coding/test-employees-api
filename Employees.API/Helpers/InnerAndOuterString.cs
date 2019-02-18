using System;

namespace Employees.API.Helpers
{
  public class InnerAndOuterString
  {
    public string inner, outer;
    public InnerAndOuterString(string inner, string outer)
    {
      this.inner = inner;
      this.outer = outer;
    }
    public override string ToString()
    {
      return String.Format("Outer: {0}\nInner: {1}", outer, inner);
    }
  }
}