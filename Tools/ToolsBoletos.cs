using SeguripassWS.Tools;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRifas.Tools
{
    public static class ToolsBoletos
    {
        public static string PedirBoleto(string name, string boleto)
        {
            try
            {

                string R=ConexionMySQL.ConsultarString(ConsultasSQL.ExisteBoleto(boleto));
                if (R == null || R=="")
                    throw new Exception();

                if (R.Equals("YES"))
                    return "Oie @"+name+" el boleto "+boleto+" ya esta ocupado :c";
                else 
                {
                    dynamic dynamic = new ExpandoObject();
                    dynamic.TipoEvento = "RegistrarBoleto";
                    dynamic.TwitchName = name;
                    dynamic.Boleto = boleto;
                    FilaEventos.LE.Add(dynamic);
                    return "Oie @" + name + " deja T aparto el boleto "+boleto+" mientras este cuei lo aprueba. uwu";
                }
                
                
            }
            catch(Exception ex)
            {
                return "Algo salio mal @" + name + " D:!!!, no te registre el boleto " + boleto+" cala en unos 2 mins mas, mientras disfruta el stream.";

            }


        }

        public static List<dynamic>  ListaBoletos()
        {
            return ConexionMySQL.ConsultarSelect(ConsultasSQL.SelectBoletos());
        }



    }
}
