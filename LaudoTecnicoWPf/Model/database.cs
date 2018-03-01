using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace LaudoTecnicoWPF.Model
{
    public class database
    {
        public SQLiteConnection conn = null;
        public DataTable dt = null;
        public SQLiteConnection Construir()
        {

            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            path = path.Substring(6);
            path += @"\laudotecnicobd.sqlite";
            String strConn = @"Data Source="+path+";Version=3;New=False;Compress=True;";
            conn = new SQLiteConnection(strConn);          
                
        
            return conn;
            
   
        }
    }

}
