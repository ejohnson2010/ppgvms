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
  public partial class CheckinForm : Window
  {       
    DispatcherTimer timer = new DispatcherTimer();
    public CheckinForm()
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
      Game game = Database.Instance.FindGameByBarcode(barcode);
      
      if (game != null) {
        txtGameBarcode.Text = game.Barcode;
        barcode = game.Barcode;
        if (MainViewModel.Instance.Checkin(barcode)) {
          UpdateStatusMessage("Check in Succeed");
          timer.Interval = TimeSpan.FromSeconds(3);
          timer.Start();
        } else {
          UpdateStatusMessage("Error Processing Checkin");
        }
      }else {
        UpdateStatusMessage("Game not Found");
      }

      MainViewModel.Instance.RefreshCheckoutHistoryData();
    }
        
    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
      timer.Tick -= TimerTick;
    }
  }
}
