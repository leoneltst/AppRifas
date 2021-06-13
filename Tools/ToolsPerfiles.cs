using AppRifas.Tools;
using SeguripassWS.Tools;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppRifas
{
    public static class ToolsPerfiles
    {
        public static string CrearPerfil(string TwitchName)
        {
            try
            {

                string respuesta = ConexionMySQL.ConsultarString(ConsultasSQL.InsertPerfiles(TwitchName));
                if (respuesta==null)
                {
                    throw new Exception();
                }
                Thread.Sleep(1000);
                return "Ahora tienes tu perfil @" + TwitchName+" UWU !!!";
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                return "Algo salio mal @" + TwitchName + " uwun't";
            }
        }
        public static string ActualizarImagen(string TwitchName, string URL)
        {
            try
            {

                string respuesta = ConexionMySQL.ConsultarString(ConsultasSQL.UpdateProfileImage(TwitchName,URL));
                if (respuesta == null)
                {
                    throw new Exception();
                }
                Thread.Sleep(1000);
                dynamic dynamic = new ExpandoObject();
                dynamic.TipoEvento = "URLChange";
                FilaEventos.LE.Add(dynamic);

                return "Haz Actualizado tu Imagen @" + TwitchName + " UWU !!!";
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                return "Algo salio mal @" + TwitchName + " uwun't";
            }
        }
        public static List<dynamic> GetPerfles()
        {
            try
            {
                List<dynamic> LD = ConexionMySQL.ConsultarSelect(ConsultasSQL.SelectPerfiles());
                return LD;
            }
            catch (Exception)
            {
                return null;
            }
        }



    }
}
