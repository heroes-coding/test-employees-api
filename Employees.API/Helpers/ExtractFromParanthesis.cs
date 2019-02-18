using System;

namespace Employees.API.Helpers
{
  public class ExtractFromParanthesis
  {
    /// <summary>
    /// Takes in an input string and a starting delimiter. 
    /// 
    /// If the input does not have the delimiter, an opening paranthesis does not follow it, or there is no closing paranthesis, nothing is returnen.
    /// 
    /// Otherwise, an <c>InnerAndOuterString<c> is returned, which contains the outer string (input minus the delimeter and its following paranthesis) and the inner string (inside the paranthesis of the delimiter)
    /// </summary>
    /// <param name="delimiter"></param>
    /// <param name="input"></param>
    /// /// <returns></returns>
    public static InnerAndOuterString fromDelimiter(string delimiter, string input)
    {
      if (delimiter == null || input == null)
      {
        return null;
      }
      int delimiterIndex = input.IndexOf(delimiter);
      if (delimiterIndex < 0)
      {
        return null;
      }
      int start = delimiterIndex + delimiter.Length;
      if (input[start] != '(')
      {
        return null;
      }
      int n = input.Length;
      start++; // increment to after paranthesis
      if (start >= n)
      {
        return null;
      }
      int openParanthesis = 1;
      int end = start;
      for (int i = start; i < n; i++)
      {
        char c = input[end];
        if (c == '(')
        {
          openParanthesis++;
        }
        else if (c == ')')
        {
          openParanthesis--;
        }
        if (openParanthesis == 0)
        {
          break;
        }
        end++;
      }
      if (openParanthesis != 0)
      {
        return null;
      }

      return new InnerAndOuterString(
      input.Substring(start, end - start),
      String.Concat(input.Substring(0, start - delimiter.Length - 1), input.Substring(end + 1, n - end - 1))
    );
    }
  }

}