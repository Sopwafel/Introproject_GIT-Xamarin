//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SQLite;

//namespace App1.database
//{
//    class KnopDB
//    {
     
//        string begintijd_opslag;
//        SzaboQL Szabo_QL;

//        /// <summary>
//        /// new_entry maakt een nieuwe regel in de tabel aan, zodra je op start klikt moet je deze aanroepen. de begintijd wordt dan ingevuld voor de begintijd en de eindtijd.
//        /// deze hang je aan een button
//        /// </summary>
//        /// <param name="begintijd"></param>
//        public void new_entry(string begintijd)
//        {
//            Szabo_QL = new SzaboQL();
//            Szabo_QL.LocalConnection("database");
        
//            begintijd_opslag = begintijd;
//            this.make_table();

//            //vul aan bij de tabel
//            string[] kolommen = { "begintijd", "eindtijd", "bezigheidId" };
//            string[] gegevens = { begintijd, begintijd, "PARSE_INT:9" };
//            Szabo_QL.Insert("Timer_input", kolommen, gegevens);
            
           
            
//        }

//        /// <summary>
//        /// Als je de timer stopt roep je deze aan en geef je de eindtijd mee.
//        /// </summary>
//        /// <param name="eindtijd"></param>
//        public void update_entry(string eindtijd)
//        {            
//            Szabo_QL.update("Timer_input", "eindtijd", eindtijd, "begintijd", begintijd_opslag);
//            Szabo_QL.close();
//        }

//        public void make_table() //dit komt in szabo_QL
//        {
//            string tabelnaam = "Timer_input";
//            string[] kolommen = {"begintijd", "eindtijd", "bezigheidId" };
//            string[] types = { "TEXT", "TEXT", "INTERGER" };
//            string primary = "begintijd";

//            Szabo_QL.create(tabelnaam,kolommen,types,primary);
//                   }
//    }
//}
