using System;
using System.Collections.Generic;
using Npgsql;
using System.Data.Common;
namespace provadb
{
    public class db
    {
        private string _connString;
        public db(string connString)
        {
            _connString = connString;
        }

        


    }
}