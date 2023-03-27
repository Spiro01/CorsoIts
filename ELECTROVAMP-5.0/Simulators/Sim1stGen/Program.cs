using Microsoft.Extensions.Configuration;
using SimpleOPC;
using Spectre.Console;

namespace Simulatore_1st_Gen
{
    public class Simulatore
    {
        const int max_pezzo = 3;

        static async Task AccensioneMacchina(SimpleOPC_Client opc)
        {
            List<Data> listWrite = new()
            {
                opc.ParseData<bool>("MachineON", true),
                opc.ParseData<bool>("MachineREADY", true)
            };

            await opc.WriteAsync(listWrite);
        }

        static async Task SpegnimentoMacchina(SimpleOPC_Client opc)
        {
            List<Data> listWrite = new()
            {
                opc.ParseData<bool>("MachineON", false),
                opc.ParseData<bool>("MachineREADY", false),
                opc.ParseData<bool>("Alarm", false)
            };

            await opc.WriteAsync(listWrite);
        }

        static async Task<bool> AzionePressa(SimpleOPC_Client opc)
        {
            var emergenza = await opc.ReadValueAsync("Alarm");

            if ((bool)emergenza.data.Value)
            {
                return false;
            }

            List<Data> listWrite = new()
            {
                opc.ParseData<bool>("MachineREADY", true),
                opc.ParseData<bool>("MachineREADY", false)
            };

            await opc.WriteAsync(listWrite[1]);

            Thread.Sleep(2000);

            await opc.WriteAsync(listWrite[0]);

            return true;
        }

        static async Task Fine_pezzo(SimpleOPC_Client opc)
        {
            List<Data> listWrite = new()
            {
                opc.ParseData<bool>("WorkDone", true),
                opc.ParseData<bool>("WorkDone", false)
            };

            await opc.WriteAsync(listWrite[0]);

            Thread.Sleep(1000);

            await opc.WriteAsync(listWrite[1]);
        }

        static async Task Emergenza(SimpleOPC_Client opc)
        {
            List<Data> listWrite = new()
            {
                opc.ParseData<bool>("MachineREADY", true),
                opc.ParseData<bool>("MachineREADY", false),
                opc.ParseData<bool>("Alarm", false),
                opc.ParseData<bool>("Alarm", true)
            };

            var emergenza = await opc.ReadValueAsync("Alarm");

            if ((bool)emergenza.data.Value)
            {
                await opc.WriteAsync(listWrite[2]);//emergenza false
                await opc.WriteAsync(listWrite[0]);//macchina pronta true

                AnsiConsole.Clear();
            }
            else
            {
                await opc.WriteAsync(listWrite[3]);//emergenza true
                await opc.WriteAsync(listWrite[1]);//macchina pronta false

                Console.WriteLine("[ Attenzione EMERGENZA attiva ]\n");
            }
        }

        static async Task Main(string[] args)
        {

            #region Setup

            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(".\\configs\\appsettings.json", optional: false, reloadOnChange: true);

            var conf = builder.Build();
            var section = conf.GetSection(nameof(Config));
            var config = section.Get<Config>();

            SimpleOPC_Client opc = new(".\\configs\\ConsoleReferenceClient.Config.xml", config); //create the istance and connect to the opc endpoint

            #endregion

            await AccensioneMacchina(opc);

            string choice;
            int pezzo = 0;

            do
            {

                string[] menu = new[]
                {
                    "Aziona Pressa",
                    "Emergenza",
                    "Spegni Macchina"
                };

                choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .AddChoices(menu)
                    .Title("Macchina Prima Generazione Accesa e Pronta"));

                switch (choice)
                {
                    case "Aziona Pressa":
                        {
                            if (AzionePressa(opc).Result)
                            {
                                pezzo++;
                            }

                            if (pezzo == max_pezzo)
                            {
                                await Fine_pezzo(opc);
                                pezzo = 0;
                            }

                            break;
                        }

                    case "Emergenza":
                        {
                            await Emergenza(opc);
                            //menu[0]+= " [ ATTENZIONE EMERGENZA ]";
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

            } while (!choice.Equals("Spegni Macchina"));

            await SpegnimentoMacchina(opc);

            await opc.DisposeAsync();

        }
    }
}