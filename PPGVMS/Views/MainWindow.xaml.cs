using PPGVMS.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PPGVMS.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private MainViewModel mainViewModel;
    public MainWindow() {
      InitializeComponent();

      mainViewModel = MainViewModel.Instance;
      DataContext = mainViewModel;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e) {
      mainViewModel.LoadLoginWindow();
      //RefreshCheckoutHistoryDataGrid();
    }         

    private void txtUserSearchName_KeyUp(object sender, KeyEventArgs e) {      
      mainViewModel.SearchUser(txtUserSearchName.Text);      
    }

    private void txtGameSearchName_KeyUp(object sender, KeyEventArgs e) {
      mainViewModel.SearchGame(txtGameSearchName.Text);
    }
  }
}
