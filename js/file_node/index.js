var fs = require('fs');

var { promisify } = require('util');

var readdir = promisify(fs.readdir);
var readFile = promisify(fs.readFile);
var writeFile = promisify(fs.writeFile);
var path = './files'


async function run() {
    try {
        var files = await readdir(path)
        var promises = [];
        chars = '';
        for (const file of files) {
            promises.push(readFile(path + '/' + file, { encoding: 'utf8' }))

        }

        var tmp = await Promise.all(promises);

        for (const text of tmp) {
            chars += text + '\n';
        }
        console.log(chars);
        run1(chars)
    } catch (e) {
        console.log('Errore')
    }

}

async function run1(text){
   
    await writeFile('./text.txt',text, { encoding: 'utf8'}).catch(err => console.log(err));
}

run()