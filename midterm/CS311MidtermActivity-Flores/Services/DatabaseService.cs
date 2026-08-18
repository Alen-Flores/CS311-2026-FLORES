//add first this library
using MySql.Data.MySqlClient;
using System.Data;


namespace CS311_CS3A_2026_Flores.Services;


public interface IDatabaseService
{
    public DataTable GetData(string sql);
    public int executeSQL(string sql);
}

// Renamed `Class1`
public class DatabaseService : IDatabaseService
{
    private string sqlConString;
    public DatabaseService(string server_address, string database, string username, string password)
    {
        sqlConString = "Server = " + server_address + "; Database = " + database + "; UId = "
        + username + "; Pwd = " + password + "; CharSet = utf8;";
    }
    //select
    public DataTable GetData(string sql)
    {
        //connection
        MySqlConnection Sqlcon = new MySqlConnection(sqlConString);
        //checking if the connection is close
        if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
        //creating a command using the connection and the sql query
        MySqlCommand SQLcom = new MySqlCommand(sql, Sqlcon);
        //creating the adapter using the created sql command
        MySqlDataAdapter SQLadap = new MySqlDataAdapter(SQLcom);
        DataSet ds = new DataSet();
        //fill the dataset using the adapter
        SQLadap.Fill(ds);
        return ds.Tables[0];
    }
    //insert, update, delete
    // Returns rows affected
    public int executeSQL(string sql)
    {
        //connection
        MySqlConnection Sqlcon = new MySqlConnection(sqlConString);
        //open the connection
        if (Sqlcon.State == ConnectionState.Closed) Sqlcon.Open();
        //build the sql command using the sql statement and the connection
        MySqlCommand SQLcom = new MySqlCommand(sql, Sqlcon);
        //execute the sql command
        return SQLcom.ExecuteNonQuery();
    }
}
