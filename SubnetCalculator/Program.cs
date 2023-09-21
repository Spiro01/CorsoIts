using System.Net;
using subnet_calculator;



var subC = new SubnetCalculator();

// var hosts = new List<(string, int)>()
// {("a",254),("b",254)};

// subt.Fill(IPAddress.Parse("172.16.0.0"), hosts);

subC.PromptFill();
subC.Elaborate();
subC.Print();
Console.ReadKey(false);