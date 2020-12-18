using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPGVMS.Data
{
  public class Boardgame
  {
    public string Name { get; set; }
    public int Year{ get; set; }
    public String Desinger { get; set; }
    public string SortKey { get; set; }

    public Boardgame()
    {
      SortKey = Name.Substring(1, 1);

    }

  }
}
