import mqtt, { MqttClient } from "mqtt";
export interface IListener {
    data: String;
    protocol : MqttClient;
    topic:string;
    
    
}