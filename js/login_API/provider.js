var { Client } = require('pg');

function getDbClient() {
    var client = new Client({
        user: 'user',
        host: 'localhost',
        database: 'auth',
        password: 'admin',
        port: 5432,
    });
    client.connect();
    return client;
}

module.exports.postlogin = async function(user, password) {
var client = getDbClient();

var result = await client.query("select id,username,role from users where username = $1 and passwd =  $2;",[user,password]);

if (result.rows.length == 0 || result.rows.length>1) return undefined;

return result.rows[0];
}