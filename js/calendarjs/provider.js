var {Client} = require('pg');

function getDbClient() {
    var client = new Client({
        user: 'calendario',
        host: 'localhost',
        database: 'calendario',
        password: 'ciao',
        port: 5432,
    });
    client.connect();
    return client;
}

module.exports.getEvents = async function() {
    var client = getDbClient();
    var result = await client.query("SELECT * FROM eventi;");
    client.end();
    return result.rows;
}

module.exports.getEvent = async function(id) {
    var client = getDbClient();
    var result = await client.query(
        "SELECT * FROM eventi WHERE id = $1;", [id]);
    client.end();
    return result.rows[0];
}

module.exports.addEvent = async function(newEvent) {
    var client = getDbClient();
    
    var insert = await client.query(
        "INSERT INTO eventi (title, startdate, enddate, location) VALUES ($1, $2, $3, $4) RETURNING id;",
    [newEvent.title, newEvent.startdate, newEvent.enddate, newEvent.location]);
    var id = insert.rows[0].id;
    
    var last = await client.query("SELECT * FROM eventi WHERE id = $1", [id]);
    console.log(last)
    client.end();
    return last.rows[0];
}

module.exports.deleteEvent = async function(id) {
    var client = getDbClient();
    var result = await client.query(
        "DELETE from eventi WHERE id = $1;", [id]);
    client.end();
    return result.rowCount > 0;
}

module.exports.editEvent = async function(post) {
    var {id, title, startdate, enddate, location} = post;
    
    var client = getDbClient();
    var result = await client.query(
        "UPDATE eventi SET title = $2, startdate = $3, enddate = $4, location = $5 WHERE id = $1;", 
        [id, title,startdate,enddate,location]);
        
    client.end();
    return result.rowCount > 0;
}
