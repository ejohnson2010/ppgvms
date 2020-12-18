using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PPGVMS.Views
{
  /// <summary>
  /// Interaction logic for LogonForm.xaml
  /// </summary>
  public partial class LogonForm : Window
  {

  
    private bool attemptLogin;
    public LogonForm()
    {
      InitializeComponent();
      DataContext = MainViewModel.Instance;
      txtUserName.Focus();
    }

    private void txtUserName_LostFocus(object sender, RoutedEventArgs e) {
      //check login
      if (attemptLogin) {
        MainViewModel.Instance.Login(txtUserName.Text);

        if (!MainViewModel.Instance.LoggedIn) {
          MessageBox.Show("Librarian not found");      
        } else {          
          Close();          
        }
      }
    }

    private void txtUserName_KeyDown(object sender, KeyEventArgs e) {
      if (e.Key == Key.Tab) {
        attemptLogin = true;
      }else {
        attemptLogin = false;
      }
    }

    private void txtUserName_GotFocus(object sender, RoutedEventArgs e) {
      attemptLogin = false;
    }
  }
}
