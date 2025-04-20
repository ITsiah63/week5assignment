using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            InsertData(sqlite_conn);
            ReadData(sqlite_conn);
        }

        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=C:;SQLiteDatabaseBrowserPortable;USERS.db;");
           // Open the connection:
         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {

            //creating the table 'users'
            SQLiteCommand sqlite_cmd;
            string Createsql = @"CREATE TABLE users 
                (id VARCHAR(20) PRIMARY KEY, 
                username VARCHAR(20) UNIQUE, 
                email VARCHAR (20) UNIQUE, 
                password VARCHAR (20) NOT NULL,
                isadmin BOOLEAN DEFAULT 0,
                createdat VARCHAR(20) DEFAULT CURRENT_TIMESTAMP
                )";
           
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            
            //creating values for the rows of data
            sqlite_cmd.CommandText = @"INSERT INTO users
               (id,username,email,password,isadmin,createdat) VALUES('1','user1','user1@email.com','Password_D','1','2023-03-26 06:19:10'); ";
           sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = @"INSERT INTO users
               (id, username, email, password, isadmin, createdat) VALUES('2','user2','user2@email.com','Password_C','0','2023-03-26 16:28:47'); ";
           sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = @"INSERT INTO users
               (id, username, email, password, isadmin, createdat) VALUES('3','user3','user3@email.com','Password_B','0','2023-03-27 06:19:10'); ";
           sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = @"INSERT INTO users
               (id, username, email, password, isadmin, createdat) VALUES('4','user4','user4@email.com','Password_A','0','2023-03-25 06:19:10'); ";
           sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM users";

            //updates user4 to become an admin
            sqlite_cmd.CommandText = "UPDATE users SET isadmin = '1' WHERE id = '4';";
            

            //execution to get all usernames from users who aren't admins
            sqlite_cmd.CommandText = "SELECT username FROM users WHERE isadmin='0' ";

            //gets id from user3 with a specific password 
            sqlite_cmd.CommandText = "SELECT id FROM users WHERE username='user3' AND password='Password_B'";

            //deleting users with id=3
            sqlite_cmd.CommandText = "DELETE FROM users WHERE id='3'";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}
