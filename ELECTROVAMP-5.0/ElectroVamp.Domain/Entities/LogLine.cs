
namespace ElectroVamp.Domain.Entities
{
    public class LogLine
    {
        public string Code { get; set; }
        public string Time_T { get; set; }
        public string Date_Time { get; set; }
        public string Spoken_Description { get; set; }
        public string  Program { get; set; }
        public int ? Quantity_Requested { get; set; }
        public int ? Quantity_Produced { get; set; }
        public LogLine(string Code,string Time_T,string Date_Time,string Spoken_Description,string Program,int Quantity_Requested,int Quantity_Produced)
        {
            this.Code = Code;
            this.Time_T = Time_T;
            this.Date_Time = Date_Time;
            this.Spoken_Description = Spoken_Description;
            this.Program = Program;
            this.Quantity_Requested = Quantity_Requested;
            this.Quantity_Produced = Quantity_Produced;
        }
    }
}
