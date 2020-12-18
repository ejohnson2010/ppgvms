using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PPGVMS.Data;
using System.Windows.Threading;

namespace PPGVMS.Views
{ 
  /// <summary>
  /// Interaction logic for Checkout.xaml
  /// </summary>
  public partial class CheckoutForm : Window
  {
    private bool gameFound = false;
    private bool userFound = false;
    private User user = new User();
    private Game game = new Game();

    DispatcherTimer timer = new DispatcherTimer();
    public CheckoutForm()
    {
      InitializeComponent();

      txtGameBarcode.Focus();

      timer.Tick += TimerTick;      
    }

    private void TimerTick(object sender, EventArgs e) {
      timer.Stop();    
      Close();
    }

    private void UpdateStatusMessage(string message, string color = "black")
    {

      switch (color)
      {
        case "red":
          lblStatusMessage.Foreground = Brushes.Red;
          break;
        default:
          lblStatusMessage.Foreground = Brushes.Black;
          break;        
      }

      lblStatusMessage.Content = message;
      lblStatusMessage.Visibility = Visibility.Visible;
    }

    private void txtGameBarcode_LostFocus(object sender, RoutedEventArgs e)
    {
      string barcode = txtGameBarcode.Text.Trim();
      game = Database.Instance.FindGameByBarcode(barcode);
      gameFound = false;

      if (game != null)
      {
        //fill in the full barcode in case it wasn't 
        txtGameBarcode.Text = game.Barcode;
        if (!game.CheckedOut) {
          lblGameSearchResult.Content = game.Name;
          gameFound = true;
        }else {
          lblGameSearchResult.Content = "Already checked out";
          return;
        }
      }else{
        lblGameSearchResult.Content = "Invalid Barcode";        
        txtGameBarcode.SelectAll();
        return;
      }

      TryCheckout();
    }
    
    private void txtUserBarcode_LostFocus(object sender, RoutedEventArgs e)
    {
      string barcode = txtUserBarcode.Text.Trim();
      user = Database.Instance.FindUserByBarcode(barcode);
      userFound = false;

      if (user != null) {
        if (user.CanCheckout) {
          lblUserSearchResult.Content = user.FullName;
          userFound = true;
        } else {
          lblUserSearchResult.Content = "User not authorized";
          txtUserBarcode.SelectAll();
          return;
        }
      }else {
        lblUserSearchResult.Content = "User not found";
        txtUserBarcode.SelectAll();
        return;
      }
      
      //user if found and authorized...can they check out?
      if (user.GameCheckedOut && (game.BaseID != user.GameCheckedOutID)){
        lblUserSearchResult.Content = "User has a game out";
        txtUserBarcode.SelectAll();
        userFound = false;
        return;
      }

      TryCheckout();
    }  

    private void TryCheckout() {
      if (userFound & gameFound) {

        if (MainViewModel.Instance.Checkout(game, user)) {
          UpdateStatusMessage("Check out Succeed");
          timer.Interval = TimeSpan.FromSeconds(2);
          timer.Start();
          MainViewModel.Instance.RefreshCheckoutHistoryData();
        } else {
          UpdateStatusMessage("Error Processin Checkout");
        }
      }
    }
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      timer.Tick -= TimerTick;
    }
  }
}
