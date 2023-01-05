using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ATM.Repositories;

namespace ATM.Services
{
    public class SQLite
    {
        public static SQLiteConnection Conn { get; set; }
        public SQLite()
        {
            Conn = CreateConnection();
            SQLiteConnection conn = Conn;
            CreateTable(Conn);            
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
                " Col1 BLOB(40)," +
                " Col2 TEXT(20)," +
                " Col3 TEXT(20)," +
                " Col4 TEXT(20)," +
                " Col5 REAL(20)," +
                " Col6 TEXT(20)," +
                " Col7 BLOB(20)," +
                " Col8 BLOB(20))";
            command = conn.CreateCommand();
            command.CommandText = createSQL;
            command.ExecuteNonQuery();
        }
        public static void InsertData(SQLiteConnection conn, object line)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO BankData(Col1, Col2, col3, col4, col5, col6, col7, col8) " +
                $"VALUES ({line});";
            command.ExecuteNonQuery();
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
