using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PPGVMS.Data;
using PPGVMS.Views;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;

namespace PPGVMS
{
  
  public class MainViewModel : INotifyPropertyChanged {
    //singleton implentation
    private static MainViewModel instance;
    private static object syncLock = new Object();
    public DispatcherTimer inactivityTimer = new DispatcherTimer();
    const int inactivityTimeout = 5;

    private MainViewModel() {
      inactivityTimer.Tick += TimerTick;

      inactivityTimer.Interval = TimeSpan.FromSeconds(5);
      inactivityTimer.Start();
    }
    private void TimerTick(object sender, EventArgs e) {

      long idleTime = IdleTimeFinder.GetIdleTime();
      long timeoutInMS = inactivityTimeout * (60000);
      
      if ((idleTime > timeoutInMS) && LoggedIn) {
        LoadLoginWindow();
      }
    }
    
    public static MainViewModel Instance{
      get {
        // First check
        if (instance == null)
        {
          lock (syncLock)
          {
            if (instance == null)
            {
              instance = new MainViewModel();
            }
          }
        }

        return instance;
      }
    }

    //properties
    private DataTable historyData = new DataTable();
    public DataTable HistoryData {
      get {
        return historyData;
      }
      set {
        historyData = value;
        OnPropertyChanged(new PropertyChangedEventArgs("HistoryData"));
      }
    }

    private DataTable gameSearchData = new DataTable();
    public DataTable GameSearchData {
      get {
        return gameSearchData;
      }
      set {
        gameSearchData = value;
        OnPropertyChanged(new PropertyChangedEventArgs("GameSearchData"));
      }
    }

    private DataTable userSearchData = new DataTable();
    public DataTable UserSearchData {
      get {
        return userSearchData;
      }
      set {
        userSearchData = value;
        OnPropertyChanged(new PropertyChangedEventArgs("UserSearchData"));
      }
    }

    private bool loggedIn = false;
    public bool LoggedIn {
      get { return loggedIn; }
    }

    public string CurrentLibrarian {
      get {
        if (loggedInLibrarian != null) {
          return String.Format("{0} {1}", loggedInLibrarian.FirstName, loggedInLibrarian.LastName);
        }else {
          return "None";
        }
      }
    }

    private User loggedInLibrarian;
    public User LoggedInLibrarian {
      get { return loggedInLibrarian; }
      set {
        loggedInLibrarian = value;
        OnPropertyChanged(new PropertyChangedEventArgs("CurrentLibrarian"));        
      }              
    }
        
    public void RefreshCheckoutHistoryData() {     
      HistoryData = Database.Instance.CheckOutHistoryQuery();
      SearchUser("");
      SearchGame("");      
      OnPropertyChanged(new PropertyChangedEventArgs("HistoryData"));
    }

    public void SearchGame(string GameName) {      
      GameSearchData = Database.Instance.SearchGame(GameName);
      OnPropertyChanged(new PropertyChangedEventArgs("GameSearchData"));
    }

    public void SearchUser(string UserName) {      
      UserSearchData = Database.Instance.SearchUser(UserName);
      OnPropertyChanged(new PropertyChangedEventArgs("UserSearchData"));
    }

    //Exit Command
    private ICommand exitCommand;
    public ICommand ExitCommand
    {
      get
      {
        if (exitCommand == null)
        {
          exitCommand = new RelayCommand(
            param => Exit(),
            param => true
          );
        }
        return exitCommand;
      }
    }

    public void Exit()
    {
      System.Windows.Application.Current.MainWindow.Close();
    }

    //Checkout Command
    private ICommand loadCheckoutCommand;
    public ICommand LoadCheckoutCommand {
      get {
        if (loadCheckoutCommand == null) {
          loadCheckoutCommand = new RelayCommand(
            param => OpenCheckout(),
            param => true
          );
        }
        return loadCheckoutCommand;
      }
    }

    public void OpenCheckout() {
      CheckoutForm checkoutForm = new CheckoutForm();
      checkoutForm.Owner = System.Windows.Application.Current.MainWindow;
      checkoutForm.ShowDialog();
    }

    //Checkint Command
    private ICommand loadCheckinCommand;
    public ICommand LoadCheckinCommand {
      get {
        if (loadCheckinCommand == null) {
          loadCheckinCommand = new RelayCommand(
            param => OpenCheckin(),
            param => true
          );
        }
        return loadCheckinCommand;
      }
    }
    public void OpenCheckin() {
      CheckinForm checkinForm = new CheckinForm();
      checkinForm.Owner = System.Windows.Application.Current.MainWindow;
      checkinForm.ShowDialog();
    }

    //Logout Command
    private ICommand logoutCommand;
    public ICommand LogoutCommand {
      get {
        if (logoutCommand == null) {
          logoutCommand = new RelayCommand(
            param => Logout(),
            param => true
          );
        }
        return logoutCommand;
      }
    }
    public void Logout() {
      loggedIn = false;
      LoggedInLibrarian = null;

      HistoryData = null;
      UserSearchData = null;
      GameSearchData = null;      

      LoadLoginWindow();

    }

    public bool Login(string userBarCode) {
      //verify login with DB           
      User lookupUser = Database.Instance.FindUserByBarcode(userBarCode);

      if (lookupUser != null && (lookupUser.IsAdmin || lookupUser.IsLibrarian)) {
        loggedIn = true;
        LoggedInLibrarian = lookupUser;
      } else {
        loggedIn = false;
        LoggedInLibrarian = null;
      }

      if (loggedIn) {
        //poplulate data
        SearchUser("");
        SearchGame("");
        RefreshCheckoutHistoryData();
      }
      
      return loggedIn;
    }

    public bool Checkout(Game game, User user) {      
      return Database.Instance.Checkout(user.ID, game.InstanceID, LoggedInLibrarian.ID);
    }
    public bool Checkin(string barcode) {      
      return Database.Instance.Checkin(barcode, LoggedInLibrarian.ID);
    }    

    public void LoadLoginWindow() {
      LogonForm logonForm = new LogonForm();
      logonForm.Owner = System.Windows.Application.Current.MainWindow;

      logonForm.ShowDialog();

      if (!LoggedIn) {
        Exit();
      }

      RefreshCheckoutHistoryData();
    }    

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(PropertyChangedEventArgs e) {
      if (PropertyChanged != null)
        PropertyChanged(this, e);
    }
  }
  internal struct LASTINPUTINFO {
    public uint cbSize;

    public uint dwTime;
  }

  public class IdleTimeFinder {
    [DllImport("User32.dll")]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [DllImport("Kernel32.dll")]
    private static extern uint GetLastError();

    public static uint GetIdleTime() {
      LASTINPUTINFO lastInPut = new LASTINPUTINFO();
      lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
      GetLastInputInfo(ref lastInPut);

      return ((uint)Environment.TickCount - lastInPut.dwTime);
    }
    /// <summary>
    /// Get the Last input time in milliseconds
    /// </summary>
    /// <returns></returns>
    public static long GetLastInputTime() {
      LASTINPUTINFO lastInPut = new LASTINPUTINFO();
      lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
      if (!GetLastInputInfo(ref lastInPut)) {
        throw new Exception(GetLastError().ToString());
      }
      return lastInPut.dwTime;
    }    
  }
}
