using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.database
{
    class Program_DB
    {
        public bool Aprogram_already_active = false;
        
      //  SQLiteConnection sqlite_conn;          // Database Connection Object
       // SQLiteCommand sqlite_cmd;             // Database Command Object
        //SQLiteDataReader sqlite_datareader;  // Data Reader Object

        SzaboQL Szabo_QL;

        public Program_DB()
        {
            Szabo_QL = new SzaboQL();
        }

        public void new_entry(string programma, string begintijd, string info)
        {
           
            if (Aprogram_already_active) { }

            else
                Aprogram_already_active = true;


            Szabo_QL.LocalConnection("database");
           

            try
            {
             //   sqlite_cmd.CommandText = "CREATE TABLE Programma_log (begintijd TEXT PRIMARY KEY , eindtijd TEXT, programma TEXT, extra_info TEXT);";
               // sqlite_cmd.ExecuteNonQuery();
            }
            catch
            {
                //doe niets, tabel bestaat al
            }

            string[] kolommen = { "begintijd", "eindtijd", "programma", "extra_info" };
            string[] data = { begintijd, begintijd, programma, info };
            Szabo_QL.Insert("Programma_log", kolommen, data);
/*
            string insertstring = $"INSERT INTO Programma_log (begintijd, eindtijd,programma,extra_info) VALUES ('{begintijd}','{begintijd}','{programma}','{info}');";
            sqlite_cmd.CommandText = insertstring;
            sqlite_cmd.ExecuteNonQuery();
            */



        }
        public void make_table()
        {
            string tabelnaam = "Programma_log";
            string[] kolommen = { "begintijd", "eindtijd", "programma", "extra_info" };
            string[] types = { "TEXT", "TEXT", "TEXT","TEXT" };
            string primary = "begintijd";
            Szabo_QL.create(tabelnaam, kolommen, types, primary);
        }

        public void update_entry(string begintijd, string eindtijd)
        {
            Szabo_QL.update("Programma_log", "eindtijd", eindtijd, "begintijd", begintijd);
          

        }
        public void closeconn()
        {
            Szabo_QL.close();
        }
    }
}
