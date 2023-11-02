
import { SerialPort } from "serialport/dist/serialport";
import * as dotenv from "dotenv";
import { Client, Message } from "azure-iot-device";
import { Mqtt as Protocol } from "azure-iot-device-mqtt";
import { logger } from "./logger";
import { IotHubMessage } from "./interfaces/iothubMessage";
dotenv.configDotenv({path: ".env"});

const port: any = new SerialPort({
  path: process.env.COMPORT!,
  baudRate: 115200,
});

const deviceConnectionString = process.env.IOTHUB_DEVICE_CONNECTION_STRING || " ";
const client = Client.fromConnectionString(deviceConnectionString, Protocol);

client.on("message", (message) => {
  logger.info(`Received message from IoT Hub: ${message.getData()}`);
  const data: IotHubMessage = JSON.parse(message.getData());

  const obj = [data.Timeout];

  logger.debug("output data: ");
  console.log(Buffer.from(obj));
  port.write(Buffer.from(obj));
});
