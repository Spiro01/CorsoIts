using ITS.QZER.Test1.Api.Models;

namespace ITS.QZER.Test1.Api.Controllers.Servicies
{
    public class LogService : IlogService
    {
        FileLoader file = new FileLoader();
        public IEnumerable<Log> GetAll()    
        {
            return file.AllLogs;
        }

        public IEnumerable<string> GetAllPrograms()
        {
            return file.AllLogs.Where(x => x.program != string.Empty).Select(x => x.program).Distinct();
            //return list.Where(x => x.program != string.Empty).GroupBy(x => x.program).Select(x => x.Key);
        }

        public IEnumerable<Log> GetLogByCode(string code)
        {
            List<Log> list = new List<Log>();
            foreach (string file in Directory.EnumerateFiles(".\\Output", "*.txt"))
            {
                list.Add(Log.Parse(File.ReadAllText(file)));
            }
            return file.AllLogs.Where(x=> x.code == code);
        }

       
    }
}

  
