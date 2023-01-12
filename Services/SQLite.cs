using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ATM.Models;

namespace ATM.Services
{
    public class SQLite
    {
        public SQLiteConnection Conn { get; set; }
        public SQLite()
        {
            Conn = CreateConnection();
            Console.WriteLine(CreateBankData(Conn));
            Console.WriteLine(CreateTransactions(Conn));
        }
        public SQLiteConnection CreateConnection()
        {
            
            Conn = new SQLiteConnection("Data Source = database.sqlite; version = 3; New = true; Compress = true;");
            try {Conn.Open();}
            catch { };
            return Conn;
        }
        public string CreateBankData(SQLiteConnection conn)
        {
            SQLiteCommand command;
            command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS BankData("+
                "Guid BLOB PRIMARY KEY," +
                "Name TEXT(20)," +
                "Last_Name TEXT(20)," +
                "Account_Number TEXT(20)," +
                "Balance REAL," +
                "Card_number TEXT(20)," +
                "Validity_Date BLOB," +
                "Blocked BLOB,"+
                "PIN TEXT(6));";
            command.ExecuteNonQuery();
            return "bank data created";
        }
        public string CreateTransactions(SQLiteConnection conn)
        {
            SQLiteCommand command;
            command = conn.CreateCommand();
            command.CommandText = "CREATE TABLE IF NOT EXISTS Transactions(" +
                "Name TEXT(20)," +
                "Last_Name TEXT(20)," +
                "Account_Number TEXT(20)," +
                "Amount REAL," +
                "Time BLOB);";
            command.ExecuteNonQuery();
            return "transaction data created";
        }
        public bool CheckIfTableEmpty(SQLiteConnection conn, string table_name)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM {table_name}";
            reader = command.ExecuteReader();
            string content = "";
            while (reader.Read())
            {
                if (reader.GetString(0)!=null) content = "true";
                else content = "false";
            }
            if (content=="true") return false;
            else return true;
        }
        //public void DeleteData(SQLiteConnection conn, string table_name) // Just in case
        //{
        //    SQLiteCommand command = conn.CreateCommand();
        //    command.CommandText = $"DELETE FROM {table_name}";
        //    command.ExecuteNonQuery();
        //}
        public void InsertBankData(SQLiteConnection conn, SQLentry e)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO BankData(Guid, Name, Last_name, Account_number, Balance, Card_number, Validity_date, Blocked, PIN) " +
                $"VALUES ('{e.ClientID}','{e.Name}','{e.LastName}','{e.AccountNumber}','{e.Balance}','{e.CardNumber}','{e.ValidityDate}','{e.Blocked}','{e.PIN}');";
            command.ExecuteNonQuery();
        }
        public void InsertTransaction(SQLiteConnection conn, Transaction e)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO Transactions(Name, Last_name, Account_number, Amount, Time) " +
                $"VALUES ('{e.Name}','{e.LastName}','{e.AccountNumber}','{e.Amount}','{e.Time}');";
            command.ExecuteNonQuery();
            command.CommandText = $"SELECT * FROM Transactions ORDER BY Time DESC LIMIT 0, 49999";
            command.ExecuteNonQuery();
        }
        public void ReadBankData(SQLiteConnection conn, int col)
        {
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM BankData";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                string readerString = reader.GetString(col);
                Console.WriteLine(readerString);
            }
        }
        public SQLentry GetBankInfo(SQLiteConnection conn,Guid id)
        {
            
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM BankData WHERE Guid='{id}'";
            reader = command.ExecuteReader();
            SQLentry entry = new();
            while (reader.Read())
            {
                Guid guid = reader.GetGuid(0);
                string name = reader.GetString(1);
                string lastname = reader.GetString(2);
                string accountnumber = reader.GetString(3);
                double balance = reader.GetDouble(4);
                string cardnumber = reader.GetString(5);
                DateOnly validity = DateOnly.Parse(reader.GetString(6));
                bool blocked = Convert.ToBoolean(reader.GetString(7));
                string pin = reader.GetString(8);
                entry = new SQLentry(guid, name, lastname, accountnumber, balance, cardnumber, validity, blocked, pin);
            }
            return entry;
        }
        public void GetTransactionInfo(SQLiteConnection conn, string accnum)
        {

            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"SELECT * FROM Transactions WHERE Account_number='{accnum}' ORDER BY Time DESC LIMIT 0, 49999;";
            reader = command.ExecuteReader();
            var list = new List <Transaction>();
            while (reader.Read())
            {
                var name = reader.GetString(0);
                var lastname = reader.GetString(1);
                var accountnumber = reader.GetString(2);
                double amount = reader.GetDouble(3);
                DateTime time = DateTime.Parse(reader.GetString(4));
                list.Add(new (name, lastname, accountnumber, amount, time));
            }
            Console.WriteLine(string.Format("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|", "Vardas", "Pavardė", "Sąskaitos numeris","Suma","Data"));
            Console.WriteLine(string.Format("|{0,-20}|{0,-20}|{0,-20}|{0,-20}|{0,-20}|","********************"));
            for (int i=0; i < 5; i++)
            {
                var current = list[i];
                Console.WriteLine(string.Format("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|{4,-20}|", current.Name, current.LastName, current.AccountNumber, current.Amount, current.Time));
            }
            Console.WriteLine(string.Format("|{0,-20}|{0,-20}|{0,-20}|{0,-20}|{0,-20}|", "********************"));
        }
        public void UpdateBankData(SQLiteConnection conn, SQLentry e)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"UPDATE BankData SET " +
                $" Name='{e.Name}', " +
                $"Last_name='{e.LastName}', " +
                $"Account_number='{e.AccountNumber}', " +
                $"Balance='{e.Balance}', " +
                $"Card_number='{e.CardNumber}', " +
                $"Validity_date='{e.ValidityDate}', " +
                $"Blocked='{e.Blocked}', " +
                $"PIN='{e.PIN}' WHERE Guid='{e.ClientID}';";
            command.ExecuteNonQuery();
        }
        public int GetTransactionCountToday(SQLiteConnection conn, string accnum)
        {
            int count = 0;
            SQLiteDataReader reader;
            SQLiteCommand command = conn.CreateCommand();
            string timestamp = DateTime.Today.ToShortDateString();
            command.CommandText = $"SELECT * FROM Transactions WHERE Account_number='{accnum}' AND Time LIKE'%{timestamp}%';";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                count++;
            }
            return count;
        }
    }
}
