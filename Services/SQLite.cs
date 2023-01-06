using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;
using ATM.Repositories;

namespace ATM.Services
{
    public class SQLite
    {
        public static SQLiteConnection Conn = CreateConnection();
        public SQLite()
        {
            if(Conn == null) CreateTable(Conn);            
        }
        public static SQLiteConnection CreateConnection()
        {
            SQLiteConnection conn = new SQLiteConnection("Data Source = database.sqlite; version = 3; New = true; Compress = true;");
            try { conn.Open(); }
            catch { }
            return conn;
        }
        private static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand command;
            string createSQL = "CREATE TABLE BankData(" +
                " Guid BLOB(40)," +
                " Name TEXT(20)," +
                " Last_Name TEXT(20)," +
                " Account_Number TEXT(20)," +
                " Balance REAL(20)," +
                " Card_number TEXT(20)," +
                " Validity_Date BLOB(20)," +
                " Blocked BLOB(20))";
            command = conn.CreateCommand();
            command.CommandText = createSQL;
            command.ExecuteNonQuery();
        }
        public static void InsertData(SQLiteConnection conn, SQLentry entry)
        {
            SQLiteCommand command = conn.CreateCommand();
            string commandText = "INSERT INTO BankData (Guid, Name, Last_name, Account_number, Balance, Card_number, Validity_date, Blocked) ";
            commandText += string.Format("VALUES (@{0},@{1},@{2},@{3},@{4},@{5},@{6},@{7})",
                        entry.ClientID,entry.Name,entry.LastName,entry.AccountNumber,entry.Balance,entry.CardNumber,entry.ValidityDate,entry.Blocked);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
            }
        }
        public static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM BankData";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string readerString = reader.GetString(0);
                Console.WriteLine(readerString);
            }

        }
    }
}
