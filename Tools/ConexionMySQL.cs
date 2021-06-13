using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pensiones.Tools;

namespace SeguripassWS.Tools
{
    public static class ConexionMySQL
    {
        

        public static DataTable Consultar(String sql)
        {
            try
            {
                string connStr = Propiedades.GetString("connStrMySQL");
                MySqlDataReader reader;
                using (var connection = new MySqlConnection(connStr))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(sql, connection))
                    {

                        reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }
        public static List<dynamic> ConsultarSelect (String sql)
        {
            string json="";
            List<dynamic> LD = new List<dynamic>();
            try
            {
                DataTable dt=Consultar(sql);

                for (int j = 0; j < dt.Rows.Count; j++) 
                {
                    json = "{";
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        
                        if (i>0)
                            json += ",";
                        json += "'" + dt.Columns[i].ColumnName + "':'";
                        if (true)
                        {

                        }
                        try
                        {
                            DateTime AuxFecha;
                            AuxFecha = (DateTime)dt.Rows[j][i];
                            json+= AuxFecha.ToString("u", DateTimeFormatInfo.InvariantInfo).Substring(0, AuxFecha.ToString("u", DateTimeFormatInfo.InvariantInfo).Length - 1)+ "'";

                        }
                        catch (Exception)
                        {
                            json += dt.Rows[j][i].ToString() + "'";

                        }

                    }
                    json += "}";
                    LD.Add(JObject.Parse(json));

                }


            }
            catch (Exception)
            {

            }


            return LD;
        
        }

        public static String ConsultarString(String sql)
        {
           
            try
            {
                string connStr = Propiedades.GetString("connStrMySQL");
                MySqlDataReader reader;
                using (var connection = new MySqlConnection(connStr))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(sql, connection))
                    {

                        reader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        string respuesta;
                        if (dt.Rows.Count == 0)
                            respuesta = "";
                        else
                            respuesta = (string)dt.Rows[0][0];
                        return respuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

        }


    }

}
