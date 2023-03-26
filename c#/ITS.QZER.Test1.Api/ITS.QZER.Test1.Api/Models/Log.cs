namespace ITS.QZER.Test1.Api.Models
{
    public class Log
    {
        public string code { get; set; }
        public string? s_descr { get; set; }
        //private DateTime? _date;
        private DateTime _date;
        public string? Data { get => _date.ToString("hh:mm:ss"); set => _date = DateTime.Parse(value); }
        public string? group { get; set; }
        public double z { get; set; }
        public int qty_produced { get; set; }
        public string? program { get; set; }

        public static Log Parse(string ins)
        {
            string[] column = ins.Split('|');

            try
            {
                return new Log { code = column[0], Data = column[3], s_descr = column[4], group = column[5], program = column[7], z = double.Parse(column[11]), };
            }
            catch { return null; }

        }

    }

    public class FileLoader
    {

        private List<Log>? _AllLogs;
        public IEnumerable<Log> AllLogs { get => _AllLogs.AsEnumerable(); }

        public FileLoader()
        {
            _AllLogs = new List<Log>();
            foreach (string file in Directory.EnumerateFiles(".\\Output", "*.txt"))
            {
                _AllLogs.Add(Log.Parse(File.ReadAllText(file)));
            }

        }
    }


}
