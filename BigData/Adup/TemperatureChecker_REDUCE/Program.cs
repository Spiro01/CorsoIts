using TemperatureChecker_REDUCE;

namespace TemperatureChecker_REDUCE
{
    public class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, int> measures = new Dictionary<string, int>();

            string line;
            //Read from STDIN


            while (!string.IsNullOrWhiteSpace((line = Console.ReadLine())))
            {
                // Data from Hadoop is tab-delimited key/value pairs
                var sArr = line.Split('\t');

                // Get the word
                string sensorId = sArr[0];
                // Get the count

                int value = Convert.ToInt32(sArr[1]);

                //Do we already have a count for the word?
                if (measures.ContainsKey(sensorId))
                {
                    //If so, increment the count
                    if (value >= 50)
                    {
                        measures[sensorId] ++;
                    }

                }
                else
                {
                    //Add the key to the collection
                    measures.Add(sensorId, 0);
                }
            }
            //Finally, emit each word and count
            foreach (var measure in measures)
            {
                //Emit tab-delimited key/value pairs.
                //In this case, a word and a count of 1.
                Console.WriteLine("{0}\t{1}", measure.Key, measure.Value);
            }
        }

    }


}