var provider = require('./provider');
const fastify = require('fastify')({ logger: true });
fastify.register(require('fastify-sensible'));
// fastify.get('/', async (request, reply) => {
//   return { hello: 'world' }
// })

fastify.post('/login', async (req, res) => {
    var body = req.body;
    var login = await provider.postlogin(body.username, body.password);
    
    if(login == undefined) {
        throw fastify.httpErrors.notFound();
    }
    var resp = {id : login.id,username:login.username, role:login.role};
    return resp; 
});
fastify.listen(8500)