import readline from 'readline';
import mqtt from 'mqtt';
import * as dotenv from 'dotenv'
dotenv.configDotenv();


const broker = process.env.BROKER ?? "";
const topic = process.env.TOPIC ?? "";


const client = mqtt.connect(broker);


const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});


rl.question('Inserisci il tuo nome utente: ', (username) => {
  console.log(`Benvenuto, ${username}! La chat è pronta.`);


  const sendMessage = (message: string) => {
    const jsonMessage = JSON.stringify({ from: username, msg: message });
    client.publish(topic, jsonMessage);
  };


  client.on('message', (topic, message) => {
    try{
    const { from, msg } = JSON.parse(message.toString());
    const timestamp = new Date().toLocaleTimeString();
    console.log(`${timestamp} - ${from}: ${msg}`);
  }catch(e){
    console.error("Wrong string format");
  }
  });


  client.subscribe(topic);


  const readInput = () => {
    rl.question(`${username}: `, (message) => {
      sendMessage(message);
      readInput();
    });
  };


  readInput();
});