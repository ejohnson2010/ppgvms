using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PPGVMS.Data
{
  public class Database
  {
    private static Database instance;
    private static object syncLock = new Object();

    private Database()
    {
    }

    public static Database Instance
    {
      get
      {
        // First check
        if (instance == null)
        {
          lock (syncLock)
          {
            if (instance == null)
            {
              instance = new Database();
            }
          }
        }

        return instance;
      }
    }

    private SqlConnection sql;

    private string SQL_CONNECTION_STRING
    {
      get
      {
        string _sqlConectionString;
        
        //_sqlConectionString = @"Data Source=" + ConfigurationManager.AppSettings["DBServer"].ToString() + ";Initial Catalog=" + ConfigurationManager.AppSettings["Database"].ToString() + ";";
        _sqlConectionString = @"Data Source=localhost\PPG; Initial Catalog=TheVault;";
        _sqlConectionString += "Integrated Security=SSPI;";
        _sqlConectionString += "Connect Timeout=10;";

        return _sqlConectionString;                
      }
    }
    
    public Game FindGameByBarcode(string barcode)
    {
      Game game = new Game();

      if (barcode.Length <= 5) {
        barcode = barcode.PadLeft(5, '0');
      }


      Connect();

      SqlCommand query = new SqlCommand("sp_search_game_by_barcode",sql);
      query.CommandType = CommandType.StoredProcedure;
      query.Parameters.AddWithValue("@barcode", barcode);
      
      DataTable table = new DataTable();
      SqlDataAdapter da = new SqlDataAdapter(query);

      da.Fill(table);

     if (table.Rows.Count == 1){
        DataRow row = table.Rows[0];
        game.InstanceID = (int)row["game_instance_id"];
        game.GameID = (int)row["game_id"];
        game.Barcode = row["barcode"].ToString();
        if (row["base_game_id"] != DBNull.Value) {
          game.BaseID = (int)row["base_game_id"];
        }

        game.Name = row["name"].ToString();
        game.CheckedOut = (bool)row["checked_out"];
      } else{
        game = null;
      }

      Disconnect();

      return game;
    }

    public User FindUserByBarcode(string barcode) {
      User user = new User();
      
      Connect();

      SqlCommand query = new SqlCommand("sp_search_user_by_barcode", sql);
      query.CommandType = CommandType.StoredProcedure;
      query.Parameters.AddWithValue("@barcode", barcode);

      DataTable table = new DataTable();
      SqlDataAdapter da = new SqlDataAdapter(query);

      da.Fill(table);

      DataRow row = null;
      if (table.Rows.Count == 1) {     
        row = table.Rows[0];
      } else if (table.Rows.Count >= 1) {
        for (int i = 0; i < table.Rows.Count;  i++) {
          if ((int)table.Rows[i]["game_type"] == 1) {
            row = table.Rows[i];
          }
        }

        if (row == null) {
          row = table.Rows[0];
        }
      }
      else {        
        return null;
      }
              
      user.ID = (int)row["user_id"];
      user.FirstName = row["first_name"].ToString();
      user.LastName = row["last_name"].ToString();
      user.IsAdmin = (bool)row["is_admin"];
      user.IsLibrarian = (bool)row["is_librarian"];
      user.CanCheckout = (bool)row["can_checkout"];
      user.GameCheckedOut = (bool)row["game_checked_out"];
      if (row["game_id"] != DBNull.Value){
        user.GameCheckedOutID = (int)row["game_id"];
      }
      


      Disconnect();

      return user;
    }
    
    public DataTable SearchGame(string GameName) {

      Connect();

      SqlCommand query = new SqlCommand("sp_game_search", sql);
      query.CommandType = CommandType.StoredProcedure;
      query.Parameters.AddWithValue("@game_name", GameName);      

      try {
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(query);
        sda.Fill(dt);

        return dt;
      } catch (SqlException ex) {
        MessageBox.Show(ex.Message, "SQL Server Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return null;
      }
      finally {
        Disconnect();
      }      
    }

    public DataTable SearchUser(string UserName) {

      Connect();

      SqlCommand query = new SqlCommand("sp_user_search", sql);
      query.CommandType = CommandType.StoredProcedure;
      query.Parameters.AddWithValue("@user_name", UserName);

      try {
        DataTable dt = new DataTable();
        SqlDataAdapter sda = new SqlDataAdapter(query);
        sda.Fill(dt);

        return dt;
      } catch (SqlException ex) {
        MessageBox.Show(ex.Message, "SQL Server Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return null;
      }
      finally {
        Disconnect();
      }
    }
    
    public DataTable CheckOutHistoryQuery() {

      try {
        Connect();
        string CmdString = string.Empty;
        using (sql) {
          CmdString = "SELECT * FROM vw_checkout_history order by ISNULL(checked_in, '2525-12-31') desc, checked_out desc";
          SqlCommand cmd = new SqlCommand(CmdString, sql);
          SqlDataAdapter sda = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable("Employee");
          sda.Fill(dt);
          return dt;
        }
      } catch (SqlException ex) {
        MessageBox.Show(ex.Message, "SQL Server Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return null;
      }
      finally {
        Disconnect();
      }      
    }

    public bool Checkin(string barcode, int librarianId) {

      Connect();
      int rdVal;

      SqlCommand query = new SqlCommand("sp_checkin", sql);
      query.CommandType = CommandType.StoredProcedure;
      query.Parameters.AddWithValue("@barcode", barcode);
      query.Parameters.AddWithValue("@librarian_id", librarianId);

      try {
        rdVal = query.ExecuteNonQuery();
      } catch (SqlException ex) {
        MessageBox.Show(ex.Message, "SQL Server Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
      }
      finally {
        Disconnect();
      }

      if (rdVal == 0) {
        return false;
      }else {
        return true;
      }

    }

    public bool Checkout(int userID, int gameInstanceID, int librarianId) {

      Connect();

      SqlCommand query = new SqlCommand("sp_checkout", sql);
      query.CommandType = CommandType.StoredProcedure;
      query.Parameters.AddWithValue("@game_instance_id", gameInstanceID);
      query.Parameters.AddWithValue("@user_id", userID);
      query.Parameters.AddWithValue("@librarian_id", librarianId);

      try {
        query.ExecuteNonQuery();
      } catch (SqlException ex) {
        MessageBox.Show(ex.Message, "SQL Server Error", MessageBoxButton.OK, MessageBoxImage.Error);
        return false;
      }
      finally {
        Disconnect();
      }

      return true;
    }

    public void Connect()
    {
      sql = new SqlConnection(SQL_CONNECTION_STRING);
      sql.Open();
    }
    public void Disconnect()
    {      
      sql.Close();
    }
  }
}
