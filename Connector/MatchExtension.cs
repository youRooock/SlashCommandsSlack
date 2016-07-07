using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Connector
{
  public static class MatchExtension
  {
    public static int GetNumbers(this Match match)
    {
      int value;
      try
      {
        value = Convert.ToInt32(string.Join("", match.ToString().ToCharArray().Where(Char.IsDigit)));
      }
      catch (FormatException e)
      {
        value = -1;
      }

      return value;
    }
  }
}