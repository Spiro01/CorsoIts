var Fastify = require('fastify');
var provider = require('./provider');
const qs = require('querystring')

var fastify = Fastify({
    logger: true,

});
fastify.register(require('fastify-sensible'));




//------------------------------------------GET---------------------------------------------------
fastify.get('/posts', async (req, res) => {

    var post = await provider.getPosts();

    return post;
});

fastify.get('/posts/:id', async (req, res) => {
    var id = req.params.id;

    var post = await provider.getPost(id);

    if (post == undefined) {
        throw fastify.httpErrors.notFound();
    }
    return post;
});

fastify.get('/users', async (req, res) => {
    var users = await provider.getUsers();
    var query = req.query;
    console.log(query);
    if (query.username == undefined && query.id == null)
        return users;

    if (query.username != undefined)
        var user = users.find(x => x.username == query.username);
    if (query.id != undefined)
        var user = users.find(x => x.id == query.id);

    if (user == null)
        throw fastify.httpErrors.notFound();

    return user;
});

fastify.get('/users/:id', async (req, res) => {
    var id = req.params.id;

    var user = await provider.getUser(id);

    if (user == undefined) {
        throw fastify.httpErrors.notFound();
    }
    return user;
});

//------------------------------------------POST---------------------------------------------------

fastify.post('/posts', async (req, res) => {
    var newPost = req.body
    var last = await provider.addPost(newPost);
    if (last == undefined) {
        throw fastify.httpErrors.internalServerError();
    }
    res.code(201);
    return last;
});

fastify.post('/likes', async (req, res) => {
    var newLike = req.body
    var check = await provider.getPost(req.body.postid);

    if (check.likes.find(x => x.id == newLike.userid) != undefined) {
        throw fastify.httpErrors.badRequest();
    }

    var last = await provider.addLike(newLike);
    if (last == undefined) {
        throw fastify.httpErrors.internalServerError();
    }
    res.code(201);
    return last;
});

fastify.post('/users', async (req, res) => {
    var newUser = req.body
    var last = await provider.addUser(newUser);
    if (last == undefined) {
        throw fastify.httpErrors.internalServerError();
    }
    res.code(201);
    return last;
});

//------------------------------------------DELETE---------------------------------------------------

fastify.delete('/posts/:id', async (req, res) => {
    var id = req.params.id;
    var last = await provider.deletePost(id);
    if (last == 0) {
        throw fastify.httpErrors.notFound();
    }
    res.code(204);
    return last;
});

fastify.delete('/likes/:id', async (req, res) => {
    var id = req.params.id;
    var last = await provider.deleteLike(id);
    if (last == 0) {
        throw fastify.httpErrors.notFound();
    }
    res.code(204);
    return last;
});

fastify.delete('/users/:id', async (req, res) => {
    var id = req.params.id;
    var last = await provider.deleteUser(id);
    if (last == 0) {
        throw fastify.httpErrors.notFound();
    }
    res.code(204);
    return last;
});

//------------------------------------------PUT---------------------------------------------------


fastify.put('/users/:id', async (req, res) => {
    var id = req.params.id;
    var { firstname = '', lastname = '', username = '' } = req.body;
    var edited = { id, firstname, lastname, username };

    if (await provider.editUser(edited)) {
        res.code(204);
        return undefined;
    }

    throw fastify.httpErrors.notFound();
});

fastify.put('/posts/:id', async (req, res) => {
    var id = req.params.id;
    var { userid = '', content = '' } = req.body;
    var edited = { id, userid, content };

    if (await provider.editPost(edited)) {
        res.code(204);
        return undefined;
    }

    throw fastify.httpErrors.notFound();
});

//------------------------------------------PATCH---------------------------------------------------

fastify.patch('/posts/:id', async (req, res) => {
    var id = req.params.id;
    var post2edit = req.body;
    var original = await provider.getPost(id);

    if (original == undefined) {
        throw fastify.httpErrors.notFound();
    }

    var edited = { ...original, ...post2edit, id };
    if (await provider.editPost(edited)) {
        res.code(204);
        return undefined;
    }

    throw fastify.httpErrors.internalServerError();
});


fastify.patch('/users/:id', async (req, res) => {
    var id = req.params.id;
    var user2edit = req.body;
    var original = await provider.getUser(id);

    if (original == undefined) {
        throw fastify.httpErrors.notFound();
    }

    var edited = { ...original, ...user2edit, id };
    if (await provider.editUser(edited)) {
        res.code(204);
        return undefined;
    }

    throw fastify.httpErrors.internalServerError();
});




async function Run() {
    try {
        var port = 8000;
        await fastify.listen(port);
        console.log(`Server listen on port ${port}`);
    } catch (error) {
        console.log('error', error);
    }
}





Run();