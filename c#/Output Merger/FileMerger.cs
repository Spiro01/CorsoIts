using FastExcel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Output_Merger
{
    public class FileMerger
    {

        public List<Log> logs;

        public FileMerger()
        {
            logs = new List<Log>();
        }
        public void read(string folder)
        {
            foreach (string file in Directory.EnumerateFiles(folder, "*.syn"))
                File.Delete(file);

            foreach (string file in Directory.EnumerateFiles(folder, "*.txt"))
            {
                logs.Add(Log.Parse(File.ReadAllText(file)));
                File.Delete(file);
            }
        }

        public void updateXlsx(string path)
        {
            FileInfo outputFile = new FileInfo(path);

            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(outputFile))
            {

                Worksheet wk = new Worksheet();
                Worksheet wk1 = new Worksheet();

                wk = fastExcel.Read("sheet1");
                wk.Read();
                var rows = wk.Rows.ToArray();

                Console.WriteLine(string.Format("Worksheet Rows:{0}", rows.Count()));

                foreach (var f in logs)
                {
                    wk1.AddRow(f.code, f.time_t, f.Date_Time, f.spoken_description, f.program, f.z);
                }

                
                fastExcel.Write(wk1, "sheet1");
                logs.Clear();
            }
        }

        public void saveCsv(string path)
        {
            throw new NotImplementedException();
            FileStream fs = new FileStream(".\\Result\\Result.csv", FileMode.Create, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //sw.WriteLine(lFile);
            }

        }

    }

    public class Log
    {
        public int code { get; set; }

        public long time_t { get; set; }
        public string Date_Time { get => _Date_Time.ToString("g"); }
        private DateTime _Date_Time;
        public string spoken_description { get; set; }
        public string program { get; set; }
        public double z { get; set; }

        public static Log Parse(string row)
        {
            var column = row.Split('|');

            return new Log
            {
                code = int.Parse(column[0]),
                time_t = long.Parse(column[2]),
                _Date_Time = DateTime.Parse(column[3]),
                spoken_description = column[4],
                program = column[7],
                z = double.Parse(column[11]),
            };
        }

    }


}
