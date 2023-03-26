
var Fastify = require('fastify');
var fs = require('fs');
var { promisify } = require('util')
var readFile = promisify(fs.readFile);
var writeFile = promisify(fs.writeFile);

var fastify = Fastify();
fastify.register(require('fastify-sensible'));



async function read() {
    try {

        var users = [];
        var tmp = await readFile('users.json', { encoding: 'utf8' })
        users = JSON.parse(tmp)
        return users;
        
    } catch (e) {
        console.log(e)
    }

}

async function save(objects) {

    var saveobj = JSON.stringify(objects)

    await writeFile('./users.json', saveobj, { encoding: 'utf8' }).catch(err => console.log(err));
}


fastify.get('/users', async (req, res) => {
    users = await read()
    return users;
});

fastify.get('/users/:id', async (req, res) => {
    users = await read()
    var user = users.find(x => x.id == req.params.id);
    if (user == undefined) {
        throw fastify.httpErrors.notFound();
    } else {
        return user;
    }

});

fastify.post('/users', async (req, res) => {
    users = await read();
    var newUser = req.body;
    if (newUser == null) {
        throw fastify.httpErrors.notFound();
        
    }
    newUser.id = users.length + 1;
    users.push(newUser);
    res.code(201);
    await save(users);
    return newUser;
});

fastify.put('/users/:id', async (req, res) => {
    users = await read();
    var user2edit = req.body;
    var id = parseInt(req.params.id, 10);
    var index = users.findIndex(x => x.id == id);
    if (index < 0) {
        throw fastify.httpErrors.notFound();
    }
    user2edit.id = id;

    users[index] = user2edit;
    save(users);
    return user2edit;
});

fastify.patch('/users/:id', async(req, res) => {
    users = await read();
    var user2edit = req.body;
    var id = parseInt(req.params.id, 10);
    var index = users.findIndex(x => x.id == id);
    if (index < 0) {
        throw fastify.httpErrors.notFound();
        
    }
    var originalUser = users[index];
    var user = { ...originalUser, ...user2edit, id };

    users[index] = user;
    save(users);
    return user;

});

fastify.delete('/users/:id',async (req, res) => {
    users = await read();
    var index = users.findIndex(x => x.id == req.params.id);
    if (index < 0) {
        throw fastify.httpErrors.notFound();
    }


    users.splice(index, 1);

    save(users);
    return "deleted";
});


// Run the server!
fastify.listen(1234, function (err, address) {
    if (err) {
        console.log(err);
        //fastify.log.error(err)
        process.exit(1)
    }
    // Server is now listening on ${address}
})


