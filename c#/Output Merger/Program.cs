using System;
using System.Linq;
namespace Output_Merger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileMerger file = new FileMerger();
            while (true) { 
            file.read(".\\Output");
            file.updateXlsx("output.xlsx");
            }
        }
    }
}
