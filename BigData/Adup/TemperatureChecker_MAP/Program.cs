
using System.Text.RegularExpressions;
using System.Linq;

namespace TemperatureChecker_MAP
{


    public class Program
    {
        static void Main(string[] args)
        {
            string line;
            //Hadoop passes data to the mapper on STDIN
            while ((line = Console.ReadLine()) != null)
            {
                var rows = line.Split("\n");

                var measurements = rows.Select(x => x.Split(","))
                    .Where(x => x.Length == 3 && x[2] != "999")
                    .Select(x => new Sensor(x[0], x[2], x[1]));

                foreach (var measurement in measurements)
                {
                    Console.WriteLine("{0}\t{1}", measurement.Id, measurement.Value);
                }
            }
        }
    }
}