using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPGVMS.Data
{
  public class Game
  {
    public int InstanceID { get; set; }
    public int GameID { get; set; }
    public int? BaseID{ get; set; }
    public string Name { get; set; }
    public string Barcode { get; set; }
    public bool CheckedOut { get; set; }
    public Game()
    {
    }
    public Game(string name)
    {
      Name = name;
    }
  }
}
