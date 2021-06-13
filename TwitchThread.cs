using AppRifas.Tools;
using Pensiones.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchBot;

namespace AppRifas
{
    public class TwitchThread
    {
        public static void DoWork()
        {
            try
            {
                string lastmessage = "asdasdasdassgsadgasdgafddghdsfhdfsghnvcbndad";
                string[] MetodosPermitidos = { "uwuneme", "SetAvatar", "rifa" };
                string myAcount = "";
                string myPass = "";
                string Target = "";
                try
                {
                    myAcount = Propiedades.GetString("myAcount");
                    myPass = Propiedades.GetString("myPass");
                    Target = Propiedades.GetString("Target");
                }
                catch (Exception)
                {

                    throw;
                }

                IrcClient client = new IrcClient("irc.twitch.tv", 6667, myAcount, myPass, Target);

                var pinger = new Pinger(client);
                pinger.Start();
                Console.WriteLine("Reading message");
                string realMessage = "";
                string Twitchname = "";
                string comando = "";
                string parametros = "";
                int firstStringPosition;
                Random r = new Random();

                string saludo = "Ya llego su lodo puerks uwu<3";
                for (int i = 0; i < r.Next(10) + 1; i++)
                    saludo += "!";
                //client.SendChatMessage(saludo);

                while (true)
                {
                    if (FilaEventos.LSalidas.Count > 0)
                    {
                        client.SendChatMessage(FilaEventos.LSalidas.First().ToString());
                        FilaEventos.LSalidas.RemoveAt(0);
                    }





                    var message = client.ReadMessage();

                    if (!String.IsNullOrWhiteSpace(message) && !lastmessage.Equals(message.ToString()))
                    {
                        lastmessage = message.ToString();
                        firstStringPosition = message.IndexOf("!");
                        if (firstStringPosition != -1)
                            Twitchname = message.Substring(1, firstStringPosition - 1);
                        firstStringPosition = message.IndexOf("#" + Target + " :");
                        if (firstStringPosition != -1)
                            realMessage = message.Substring(firstStringPosition + 3 + Target.Length, message.Length - (firstStringPosition + 3 + Target.Length));

                        if (realMessage.StartsWith('!'))
                        {

                            if (realMessage.Contains(" "))
                            {
                                firstStringPosition = realMessage.IndexOf(" ");
                                comando = realMessage.Substring(1, firstStringPosition - 1);
                                parametros = realMessage.Substring(firstStringPosition, realMessage.Length - (firstStringPosition));
                                while (parametros.StartsWith(' '))
                                {
                                    parametros = parametros.Substring(1, parametros.Length - 1);
                                }

                            }
                            else
                            {
                                comando = realMessage.Substring(1, realMessage.Length - 1);
                            }
                            if (MetodosPermitidos.Contains(comando, StringComparer.OrdinalIgnoreCase))
                            {

                                int Aux = 0;
                                comando = comando.ToLower();
                                switch (comando)
                                {

                                    //////////////////////////////////////
                                    case "uwuneme":
                                        client.SendChatMessage(ToolsPerfiles.CrearPerfil(Twitchname));
                                        break;

                                    //////////////////////////////////////
                                    case "setavatar":
                                        if (parametros.Equals(""))
                                            client.SendChatMessage("Este comando debe incluir parametros ejemplo: \"!" + comando + " *soy un URL*\" ");
                                        else
                                            client.SendChatMessage(ToolsPerfiles.ActualizarImagen(Twitchname, parametros));
                                        break;

                                    //////////////////////////////////////
                                    case "rifa":
                                        if (parametros.Equals("") || !Int32.TryParse(parametros, out Aux) || Aux > 100 || Aux < 1)
                                            client.SendChatMessage("Este comando debe incluir parametros ejemplo: \"!" + comando + " *numero valido*\" ");
                                        else
                                        {
                                            client.SendChatMessage(ToolsBoletos.PedirBoleto(Twitchname, parametros));


                                        }


                                        break;
                                    //////////////////////////////////////
                                    default:
                                        break;
                                }

                                Console.WriteLine(comando);
                            }
                        }

                        if (!message.Contains("mybottid"))//Para cuando es del bot
                        {

                        }

                        Console.WriteLine($"Message: {message}");
                    }

                }
            }
            catch (Exception)
            {

                DoWork();
            }
        }

        
    }
}
