using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRifas.Tools
{
    public static class ConsultasSQL
    {
        private static string R="";
        public static string InsertPerfiles(string TwitchName)
        {
            R = "INSERT INTO rifasbb.perfiles (TwitchName, FechaCreacion, UrlImage) " +
                "  SELECT '"+ TwitchName + "',current_timestamp(),'https://lh3.googleusercontent.com/X1qWXvvSE9BB7KAQLjr8MQkSt6IixVzF5JWC6a1T8psNWtj8qmwzzMYgMdSkk6yymul94z6zggBIyyjXHC0yuuIWNA=w640-h400-e365-rj-sc0x00ffffff' FROM DUAL" +
                " WHERE NOT EXISTS" +
                "  (SELECT * FROM rifasbb.perfiles WHERE TwitchName = '" + TwitchName + "');";
            return R;
        }

        public static string ExisteBoleto(string voleto)
        {
            return "SELECT IF( EXISTS(" +
                "             SELECT * " +
                "             FROM rifasbb.voletos " +
                "             WHERE `NumeroVoleto` = '" + voleto + "'  ), 'YES', 'NO')";
        }

        public static string UpdateProfileImage(string TwitchName, string URL)
        {
            R = "UPDATE `rifasbb`.`perfiles`" +
                "SET" +
                "`UrlImage` = '" + URL + "'" +
                "WHERE `TwitchName`= '" + TwitchName + "' ; ";
            return R;
        }
        public static string SelectPerfiles()
        {
            R = "select * from `rifasbb`.`perfiles`;";
            return R;
        }

        internal static string SelectBoletos()
        {
            return "SELECT A.TwitchName, A.NumeroVoleto, B.UrlImage  " +
                "FROM rifasbb.voletos as A " +
                "left join   rifasbb.perfiles as B " +
                "on A.TwitchName = B.TwitchName  " +
                "Where A.ESTADO = 'ACTIVADO' " +
                "order by CAST(A.NumeroVoleto AS UNSIGNED)  ASC; ";
        }

        public static string SelectPerfil(string name)
        {
            R = "select * from `rifasbb`.`perfiles` where TwitchName='"+name+"';";
            return R;
        }

        internal static string RegistrarBoleto(string name, string voleto)
        {
            R = "INSERT INTO `rifasbb`.`voletos`" +
                "(`TwitchName`, `ESTADO`, `NumeroVoleto`,FechaCanje) " +
                "VALUES ('"+name+"', 'ACTIVADO', '"+voleto+ "', current_timestamp()); ";
            return R;
        }
    }
}
