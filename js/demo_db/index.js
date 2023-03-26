
var { Client } = require('pg');

const client = new Client({
    user: 'spiro',
    host: '192.168.220.52',
    database: 'tickets',
    password: 'spiro',
    port: 5432,
  })

  client.connect();

  var res = await client.query("SELECT * FROM tickets");
  console.log(res);
client.end(); 