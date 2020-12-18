using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPGVMS.Data
{
  public class User   
  {
    public int ID { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsLibrarian { get; set; }
    public bool CanCheckout { get; set; }
    public bool GameCheckedOut { get; set; } 
    public int? GameCheckedOutID { get; set; }
    public string FullName { get { return FirstName + " " + LastName; }  }
    public User(string userName)
    {
      UserName = userName;
    }
    public User() {
    }
  }
}
