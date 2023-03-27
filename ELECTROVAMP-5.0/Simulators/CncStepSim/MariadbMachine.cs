using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CncStepSim
{
    public class MariadbMachine
    {
        public MySqlConnection conn { get; set; }
        Random ran = new Random();
        public MariadbMachine(string conn_string)
        {
            conn = new MySqlConnection(conn_string);
            conn.Open();
        }

        public async Task AddWork(string machineid, int programid)
        {

            var program = new { MACHINE_ID = machineid, PROGRAM_ID = programid, ROBOT = 0, PARTS_DONE = 0 };
            await conn.ExecuteAsync(@"
            insert into `WORKS` (MACHINE_ID,PROGRAM_ID,ROBOT,PARTS_DONE,START,END)
                       VALUES (@MACHINE_ID,@PROGRAM_ID,@ROBOT,@PARTS_DONE,UNIX_TIMESTAMP(),UNIX_TIMESTAMP())"
            , program
            );
        }
        public async Task<int> AddProgram(string name)
        {
            var start = DateTimeOffset.Now.ToUnixTimeSeconds();
            var send = new { NAME = name, STEPS_NUMBER = ran.Next(1, 10), PARTS_TO_DO = ran.Next(1, 50), SCHEDULED_START = start };
            await conn.ExecuteAsync(@"
            insert into `PROGRAMS` (NAME,STEPS_NUMBER,PARTS_TO_DO,PRIORITY,SCHEDULED_START,PIECE_TIME,SCHEDULED_TYPE,CODE)
                            VALUES (@NAME,@STEPS_NUMBER,@PARTS_TO_DO,100,@SCHEDULED_START,0,0,'TEST');", send);
            var result = await conn.QueryAsync<int>("SELECT LAST_INSERT_ID();");
            return result.First();

        }

        public async Task<int> PieceDone(string machineid, int delay)
        {
            await conn.ExecuteAsync($"update `WORKS` w set START = UNIX_TIMESTAMP() where MACHINE_ID = {machineid};");
            await Task.Delay(delay);
            await conn.ExecuteAsync($"update `WORKS` w set END = UNIX_TIMESTAMP(), TIME = END - START, PARTS_DONE=PARTS_DONE+1 where MACHINE_ID = {machineid};");
            var result = await conn.QueryAsync<int>($"SELECT w.PARTS_DONE FROM `PROGRAMS` p LEFT JOIN `WORKS` w on (p.ID = w.PROGRAM_ID) where w.MACHINE_ID ={machineid};");
            return result.SingleOrDefault();
        }
        public async Task<bool> ProgramDone(string machineid)
        {
            var result = await conn.QueryAsync<int>(@$"
            select p.id FROM `PROGRAMS` p LEFT JOIN `WORKS` w ON (w.PROGRAM_ID = p.ID) where  PARTS_TO_DO <= PARTS_DONE and MACHINE_ID = {machineid} ;");
            return result.FirstOrDefault() > 0 ? true : false;

        }

        public async Task ResetDb(string programname)
        {
            await conn.ExecuteAsync($"delete p,w FROM `PROGRAMS` p LEFT JOIN `WORKS` w ON (w.PROGRAM_ID = p.ID)  WHERE p.NAME = '{programname}'");
        }
    }
}
