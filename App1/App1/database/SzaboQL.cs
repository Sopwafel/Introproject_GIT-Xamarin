using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace App1.database
{
    class SzaboQL
    {
        public SzaboQL()
        { }

        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;


        public void LocalConnection(string database_name)
        {


            string connstring = "Data Source=";
            connstring += database_name;
            if (connstring.Contains(".sqlite") == false)
                connstring += ".sqlite;";
            else
                connstring += ";";

            connstring += "Version=3;";
            sqlite_conn = new SQLiteConnection(connstring);
            //sqlite_conn.Open();
            //sqlite_cmd = sqlite_conn.CreateCommand();
        }

        public void Insert(string Tabelnaam, string[] kolommen, string[] data)
        {


            string insertstring = "INSERT INTO " + Tabelnaam + " (";

            //Kijk of voor elke kolom een waarde is ingesteld
            if (data.Length != kolommen.Length)
            {

                //THROW EXCEPTION

            }

            for (int i = 0; i < kolommen.Length - 1; i++)
            {
                insertstring += kolommen[i] + ", ";
            }

            insertstring += kolommen[kolommen.Length - 1] + ") ";
            insertstring += "VALUES (";

            for (int i = 0; i < data.Length - 1; i++)
            {

                insertstring += IntOrString(data[i]) + ", ";
            }

            insertstring += IntOrString(data[data.Length - 1]) + ");";
            SQLiteCommand command = sqlite_conn.CreateCommand(insertstring);
            // sqlite_cmd.CommandText = insertstring;
            command.ExecuteNonQuery();
        }


        public void update(string tabel, string aan_te_passen_kolom, string nieuwe_waarde, string kolom_voorwaarde, string waarde_voorwaarde)
        {
            string updatestring = "UPDATE " + tabel + " SET ";
            updatestring += aan_te_passen_kolom + " =";

            updatestring += IntOrString(nieuwe_waarde);
            updatestring += " WHERE " + kolom_voorwaarde + "=";

            updatestring += IntOrString(waarde_voorwaarde);

            updatestring += ";";
            SQLiteCommand command = sqlite_conn.CreateCommand(updatestring);
            //sqlite_cmd.ExecuteNonQuery();
        }

        public void create(string tabelnaam, string[] kolomnamen, string[] types, string primary_key)
        {
            //ERROR CHECK STUKJE:
            if (kolomnamen.Length != types.Length)
                goto stop; //error: not all colums have a dedicate type or vice versa
            if (Array.IndexOf(kolomnamen, primary_key) < 0 && primary_key != "")
                goto stop;  //error: primary key not in table

            string createstring = "CREATE TABLE " + tabelnaam + " (";

            for (int i = 0; i < kolomnamen.Length - 1; i++)
            {
                createstring += kolomnamen[i] + " " + types[i] + IsPrimary(kolomnamen[i], primary_key) + ", ";
            }
            createstring += kolomnamen[kolomnamen.Length - 1] + " " + types[kolomnamen.Length - 1] + IsPrimary(kolomnamen[kolomnamen.Length - 1], primary_key) + ");";

            SQLiteCommand command = sqlite_conn.CreateCommand(createstring);
            // sqlite_cmd.CommandText = createstring;

            try { command.ExecuteNonQuery(); }
            catch { /*Tabel bestaat al */}

            stop:;
        }

        public void close()
        {
            sqlite_conn.Close();
        }

        public string IsPrimary(string naam, string primary)
        {
            if (naam == primary)
                return " PRIMARY KEY";
            return " ";
        }

        public string IntOrString(string input)
        {
            string result = "";
            if (input.Contains("PARSE_INT"))
                result += input.Replace("PARSE_INT:", "");
            else
                result += "'" + input + "'";
            return result;
        }
    }
}
