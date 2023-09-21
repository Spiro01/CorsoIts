import { ISend } from "./ISend";
import { createClient } from 'redis' ;
import { generateData } from "./Helper/DataGenerator";
import dotenv from "dotenv";
import { IListener } from "./IListener";
import { setDefaultResultOrder, TIMEOUT } from "dns";

dotenv.config();
const url = process.env.REDIS_URL ?? "localhost:6379";
const client = createClient({ socket: {
  host: 'localhost',
  port: 6379
},password:"eYVX7EwVmmxKPCDmwMtyKVge8oLd2t81"});
client.on('error', (err) => console.log('Redis Client Error', err))
client.connect()
/*
const receiveData = async()=>{
  let receive = {} as IListener;
  receive.protocol = redis.connect();
    receive.topic='droni'
    receive.protocol.subscribe(receive.topic)
    receive.protocol.on('message', (topic, payload,packet) => {
      
      console.log('Received Message:', topic, payload.toString())

      console.log(packet)
    })
}
*/

const writeData = async () => {
  let send = {} as ISend;
  

  try {
    

    
    send.data = generateData();
    const json = JSON.stringify(send.data);
    
    await client.lPush("Droni",json);
    
  } catch (ex) {
    console.error(ex);
  }
};
const sendData= async () => {
  let send = {} as ISend;
  

  try {
    let pop;
    pop = await  client.blPop("Droni",10);
   console.log(pop);
  } catch (ex) {
    console.error(ex);
  }
};
sendData();
setInterval(async () => {
  await writeData();
}, 1000);
