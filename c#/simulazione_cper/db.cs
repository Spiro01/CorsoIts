using System;
using System.Collections.Generic;
using Npgsql;
using System.Data.Common;
namespace simulazione_cper
{
    public class db
    {
        private string _connString;
        public db(string connString)
        {
            _connString = connString;
        }

        public List<ordine> GetOrdini(){
            List<ordine>ordini = new List<ordine>();

            using (DbConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select tavolo_id,nome,qta,ora from ordini;";
                DbDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read()){
                    ordine or = new ordine();
                    DateTime ora = rdr.GetDateTime(3);
                    or.tavolo = rdr.GetInt32(0);
                    bool flag = true;
                    for (int i = 0 ; i<ordini.Count;i++){
                        if(ordini[i].tavolo == or.tavolo){

                            ordini[i].ordini.Add(rdr.GetString(1));
                            ordini[i].qta.Add(rdr.GetInt32(2));
                            ordini[i].ora = ora;
                            flag = false;
                            break;
                        }

                    }
                            if(flag){
                            or.ordini.Add(rdr.GetString(1));
                            or.qta.Add(rdr.GetInt32(2));
                            or.ora = ora;
                            ordini.Add(or);
                            }
                }


                return ordini;


            }
        }

        public void SetOrdini (List<ordine> ordini){

            using (DbConnection conn = new NpgsqlConnection(_connString)){
                conn.Open();
               
                foreach(ordine o in ordini){
                    for(int i = 0; i<o.ordini.Count;i++){
                DbCommand cmd = conn.CreateCommand();
                cmd.CommandText = 
                $@"insert into ordini (tavolo_id,nome,ora,qta) values (@tavolo,@nome,@orario,@qta);";
                DbParameter tavolo = cmd.CreateParameter();
                tavolo.ParameterName = "tavolo";
                tavolo.Value = o.tavolo;
                cmd.Parameters.Add(tavolo);
                
                DbParameter nome= cmd.CreateParameter();
                nome.ParameterName = "nome";
                nome.Value = o.ordini[i];
                cmd.Parameters.Add(nome);
                
                DbParameter orario= cmd.CreateParameter();
                orario.ParameterName = "orario";
                orario.Value = o.ora;
                cmd.Parameters.Add(orario);

                
                DbParameter qta= cmd.CreateParameter();
                qta.ParameterName = "qta";
                qta.Value = o.qta[i];
                cmd.Parameters.Add(qta);

                 cmd.ExecuteNonQuery();

                }
                }

            }

        }
         public void DelOrdini (int tavolo){

            using (DbConnection conn = new NpgsqlConnection(_connString)){
                conn.Open();
                DbCommand cmd = conn.CreateCommand();
                 cmd.CommandText = 
                $@"delete from ordini where tavolo_id = {tavolo}";
                cmd.ExecuteNonQuery();

    }}
}}