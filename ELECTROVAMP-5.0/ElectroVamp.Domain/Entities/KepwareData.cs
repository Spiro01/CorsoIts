namespace ElectroVamp.Domain.Entities
{
    public class KepwareData
    {
        public string MachineId { get; set; }
        public bool Alarm { get; set; }
        public bool Warning { get; set; }
        public bool Other { get; set; }
        public bool Working { get; set; }
        public ushort Counter { get; set; }
        public ushort CycleTime { get; set; }
        public string PNC { get; set; }

        public KepwareData()
        {

        }

        public KepwareData(string filepath, int CODE, string PROGRAM)
        {
            this.MachineId = filepath;
            this.Counter = 0;
            this.CycleTime = 0;
            this.PNC = PROGRAM;
            this.Working = false;
            this.Alarm = false;
            this.Warning = false;
            this.Other = false;


            if (CODE == 200 || CODE == 201)
            {
                this.Alarm = true;
            }
            else if (CODE > 500 && CODE < 900)
            {
                this.Other = true;
            }
            else if (CODE > 350 && CODE < 400)
            {
                this.Warning = true;
            }
            else if (CODE > 300 && CODE < 350)
            {
                this.Working = true;
            }
            else
            {
                this.Other = true;
            }
        }

    }


}
