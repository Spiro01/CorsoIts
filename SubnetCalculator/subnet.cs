using System;
using System.Net;
using Spectre.Console;

namespace subnet_calculator
{
    public class Subnet
    {
        public string Nome { get; set; } //nome rete
        public int NumHost { get; set; } //numero host richiesti
        public IPAddress BroadcastIp { get; set; }  // indirizzo broadcast host
        public int bhost { get { return ElaborateFields().Item1; } }
        public int SubnetMask { get { return ElaborateFields().Item2; } }  //subnet mask (ex. /10)
        public IPAddress Ip { get; set; }  //first ip
        public int BitxHost { get; set; }
        public int NumHostMaxSubnetMask { get { return ElaborateFields().Item3; } }  //numero host massimi data da sm
        public string SubnetEstesa { get { return subnetmask_est(SubnetMask); } }  //subnet estesa



        private Tuple<int, int, int> ElaborateFields()
        {
            int n = 0;
            int bhost = 0, subnetmask, numHostMaxSubnetMask;


            for (int i = 0; n < NumHost + 2; i++)
            {
                n = int.Parse(Math.Pow(2, i).ToString());
                bhost = i;
            }
            subnetmask = 32 - bhost;
            numHostMaxSubnetMask = n - 2;

            var res = Tuple.Create(bhost, subnetmask, numHostMaxSubnetMask);
            return res;
        }

        private static string subnetmask_est(int Subnet)
        {
            int r = 0;
            string s = "";
            for (int i = Subnet; i >= 8; i = i - 8)
            {
                r++;
                s = s + "255.";
                Subnet = i - 8;
            }
            s = s + Convert.ToString(256 - (256 / (Math.Pow(2, Subnet))));
            r++;
            for (int k = 4 - r; k > 0; k--)
            {
                s = s + ".0";
            }
            return s;

        }


    }

    public class SubnetCalculator
    {
        private List<Subnet> _subnet;

        public SubnetCalculator()
        {
            _subnet = new List<Subnet>();
        }

        public void PromptFill()
        {
            bool flag = true;

            Console.WriteLine("Inserisci l'indirizzo da cui vuoi partire: ");
            string firstIp = Console.ReadLine();

            while (flag)
            {
                Subnet s = new Subnet();

                s.Ip = IPAddress.Parse(firstIp);

                Console.WriteLine("Inserisci il nome della rete:");
                s.Nome = Console.ReadLine();

                Console.WriteLine("Inserisci il numero di host");
                int nhost;
                while (!int.TryParse(Console.ReadLine(), out nhost)) { }
                s.NumHost = nhost;

                _subnet.Add(s);

                Console.WriteLine("Vuoi inserire un'altra rete? [Y/n]");
                if (Console.ReadLine() == "n") flag = false;

            }
        }

        public void Fill(IPAddress startIp, List<(string, int)> hostname)
        {
            foreach (var el in hostname)
            {
                _subnet.Add(new Subnet() { Nome = el.Item1, NumHost = el.Item2 });
            }
            _subnet[0].Ip = startIp;
        }

        public void Elaborate()
        {
            for (int i = 1; i < _subnet.Count; i++)
            {
                _subnet[i].Ip = subcalc.last_host(_subnet[i - 1].Ip, _subnet[i - 1].NumHostMaxSubnetMask + 2);
            }
            for (int i = 0; i < _subnet.Count; i++)
            {
                _subnet[i].BroadcastIp = subcalc.last_host(_subnet[i].Ip, _subnet[i].NumHostMaxSubnetMask + 1);


                _subnet[i].BitxHost = 32 - _subnet[i].SubnetMask;
            }
        }

        public void Print()
        {
            Console.Clear();

            // Create a table
            var table = new Table();

            // Add some columns

            table.AddColumn(new TableColumn("NOME").Centered());
            table.AddColumn(new TableColumn("NHOST").Centered());
            table.AddColumn(new TableColumn("BIT x HOST").Centered());
            table.AddColumn(new TableColumn("SM").Centered());
            table.AddColumn(new TableColumn("SUBNET").Centered());
            table.AddColumn(new TableColumn("BROADCAST").Centered());
            table.AddColumn(new TableColumn("SUBNET CONVERTITA").Centered());


            // Add some rows



            // Render the table to the console


            for (int i = 0; i < _subnet.Count; i++)
            {


                if (i % 2 == 0) { table.AddRow("[blue]" + _subnet[i].Nome + "[/]", "[blue]" + _subnet[i].NumHost + "[/]", "[blue]" + _subnet[i].BitxHost + "[/]", "[blue]" + _subnet[i].SubnetMask + "[/]", "[blue]" + _subnet[i].Ip + "[/]", "[blue]" + _subnet[i].BroadcastIp + "[/]", "[blue]" + _subnet[i].SubnetEstesa + "[/]"); }

                else
                { table.AddRow("" + _subnet[i].Nome, "" + _subnet[i].NumHost, "" + _subnet[i].BitxHost, "" + _subnet[i].SubnetMask, "" + _subnet[i].Ip, "" + _subnet[i].BroadcastIp, "" + _subnet[i].SubnetEstesa); }

            }
            AnsiConsole.Write(table);
        }

    }

    internal static class subcalc
    {



        public static IPAddress last_host(IPAddress Ip, int Subnet)
        {
            var splittedIP = Ip.SplitIpAddress();
            
            while (Subnet > 0)
            {
                if (splittedIP[3] < 255)
                {
                    splittedIP[3] = splittedIP[3] + 1;
                    Subnet--;
                }
                else if (splittedIP[2] < 255)
                {
                    splittedIP[2] = splittedIP[2] + 1;
                    splittedIP[3] = 0;
                    Subnet--;

                }
                else if (splittedIP[1] < 255)
                {
                    splittedIP[1] = splittedIP[1] + 1;
                    splittedIP[2] = 0;
                    Subnet--;

                }
                else if (splittedIP[0] < 255)
                {
                    splittedIP[0] = splittedIP[0] + 1;
                    splittedIP[1] = 0;
                    Subnet--;

                }
                else { throw new ArgumentOutOfRangeException("Indirizzi ip terminati"); }
            }
            return splittedIP.MergeIpAddress();
        }




        public static string Gateway(string s1, string s2)
        {

            string s3 = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].Equals('0') || s2[i].Equals('0'))
                {
                    s3 = s3 + "0";
                }
                else { s3 = s3 + "1"; }


            }
            return s3;
        }




    }


}
