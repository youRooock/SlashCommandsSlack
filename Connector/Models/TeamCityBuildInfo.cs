using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connector.Models
{
  public class TeamCityBuildInfo
  {
    public string BuildId;
    public int Passed;
    public int Failed;
    public int Errors;
  }
}
