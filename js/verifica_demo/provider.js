var { Client } = require('pg');

function getDbClient() {
    var client = new Client({
        user: 'verifica',
        host: 'localhost',
        database: 'mysocial',
        password: 'verifica',
        port: 5432,
    });
    client.connect();
    return client;
}

//------------------------------------------GET---------------------------------------------------

module.exports.getPosts = async function () {
    var client = getDbClient();
    var result = await client.query("select posts.id,posts.userid,posts.content, count(likes.id) as likes from posts full outer join likes on (posts.id = likes.postid) group by posts.content,posts.id,posts.userid order by posts.id;");
    client.end();
    return result.rows;
}

module.exports.getPost = async function (id) {
    var client = getDbClient();
    var resultpost = await client.query(
        "SELECT * FROM posts WHERE id = $1;", [id]);
    if (resultpost.rows.length == 0) return undefined;

    var resultlikes = await client.query("select users.id,users.firstname,users.lastname,users.username from posts join likes on (posts.id = likes.postid) join users on (users.id = likes.userid) where posts.id = $1;", [id]);

    var result = resultpost.rows[0];
    result.likes = resultlikes.rows;
    client.end();
    return result;
}


module.exports.getUsers = async function () {
    var client = getDbClient();
    var result = await client.query("select users.id,users.firstname,users.lastname,users.username,count(posts.id) as posts  from users full outer join posts on (users.id = posts.userid) group by users.id,users.firstname,users.lastname,users.username order by users.id;");
    client.end();
    return result.rows;
}

module.exports.getUser = async function (id) {
    var client = getDbClient();
    var resultuser = await client.query(
        "SELECT * FROM users WHERE id = $1;", [id]);
    if (resultuser.rows.length == 0) return undefined;
    var resultposts = await client.query("select posts.id,posts.userid,posts.content,count(likes.postid) as likes from users  join posts on (users.id = posts.userid) full outer join likes on (likes.postid = posts.id)  WHERE users.id = $1 group by posts.id,posts.userid,posts.content order by posts.id;", [id]);
    var result = resultuser.rows[0];
    result.posts = resultposts.rows;
    client.end();
    return result;
}

//------------------------------------------POST---------------------------------------------------

module.exports.addPost = async function (newPost) {
    var client = getDbClient();

    var insert = await client.query(
        "INSERT INTO posts (userid, content) VALUES ($1, $2) RETURNING id;",
        [newPost.userid, newPost.content]);
    var id = insert.rows[0].id;

    var last = await client.query("SELECT * FROM posts WHERE id = $1", [id]);

    client.end();
    return last.rows[0];
}

module.exports.addLike = async function (newLike) {
    var client = getDbClient();
    
    var insert = await client.query(
        "INSERT INTO likes (userid, postid) VALUES ($1, $2) RETURNING id;",
        [newLike.userid, newLike.postid]);
    var id = insert.rows[0].id;

    var last = await client.query("SELECT * FROM likes WHERE id = $1", [id]);

    client.end();
    return last.rows[0];
}

module.exports.addUser = async function (newUser) {
    var client = getDbClient();

    var insert = await client.query(
        "INSERT INTO users (firstname, lastname,username) VALUES ($1, $2, $3) RETURNING id;",
        [newUser.firstname, newUser.lastname, newUser.username]);
    var id = insert.rows[0].id;

    var last = await client.query("SELECT * FROM users WHERE id = $1", [id]);
    
    client.end();
    return last.rows[0];
}

//------------------------------------------DELETE---------------------------------------------------

module.exports.deletePost = async function(id) {
    var client = getDbClient();
    var result = await client.query(
        "DELETE from posts WHERE id = $1;", [id]);
    client.end();
    return result.rowCount > 0;
}

module.exports.deleteLike = async function(id) {
    var client = getDbClient();
    var result = await client.query(
        "DELETE from likes WHERE id = $1;", [id]);
    client.end();
    return result.rowCount > 0;
}

module.exports.deleteUser = async function(id) {
    var client = getDbClient();
    var result1 = await client.query("DELETE from posts where userid=$1;",[id]);
    
    var result2 = await client.query("DELETE from likes where userid=$1;", [id]);
    
    
    var result = await client.query(
        "delete from users where id=$1", [id]);
    client.end();
    return result.rowCount > 0;
}

//------------------------------------------PUT/PATCH---------------------------------------------------

module.exports.editUser = async function(post) {
    var {id, firstname, lastname, username} = post;
    
    var client = getDbClient();
    
    var result = await client.query(
        "UPDATE users SET firstname = $2, lastname = $3, username = $4 WHERE id = $1;", 
        [id, firstname,lastname,username]);
        
    client.end();
    return result.rowCount > 0;
}

module.exports.editPost = async function(post) {
    var {id,userid,content} = post;
    
    var client = getDbClient();
    
    var result = await client.query(
        "UPDATE posts SET userid = $2, content = $3 WHERE id = $1;", 
        [id, userid,content]);
        
    client.end();
    return result.rowCount > 0;
}