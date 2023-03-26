var Fastify = require('fastify');
var provider = require('./provider');

var fastify = Fastify({logger: true});
fastify.register(require('fastify-sensible'));

fastify.get('/events', async (req, res) => {
    var events = await provider.getEvents();

    return events;
});

fastify.get('/events/:id', async (req, res) => {
    var id = req.params.id;
    
    var events = await provider.getEvent(id);

    if(events == undefined) {
        throw fastify.httpErrors.notFound();
    }
    return events;
});

fastify.post('/events', async (req, res) => {
    var newEvent = req.body;
    var last = await provider.addEvent(newEvent);
    if(last == undefined) {
        throw fastify.httpErrors.internalServerError();
    }
    res.code(201);
    return last;
});

fastify.delete('/events/:id', async (req, res) => {
    var id = req.params.id;
    var last = await provider.deleteEvent(id);
    if(last == 0) {
        throw fastify.httpErrors.notFound();
    }
    res.code(204);
    return last;
});

fastify.put('/events/:id', async (req, res) => {
    var id = req.params.id;
    var {title = '', startdate = '', enddate = '',location = ''} = req.body;
    var edited = {id, title, startdate, enddate, location};
    
    if(await provider.editEvent(edited)) {
        res.code(204);
        return undefined;
    }

    throw fastify.httpErrors.notFound();
});

fastify.patch('/events/:id', async (req, res) => {
    var id = req.params.id;
    var event2edit = req.body;
    var original = await provider.getEvent(id);

    if(original == undefined) {
        throw fastify.httpErrors.notFound();
    }

    var edited = {...original, ...event2edit, id}; 
    if(await provider.editEvent(edited)) {
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
        console.log('errror', error);
    }
}

Run();