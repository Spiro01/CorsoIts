import { Axios, AxiosResponse } from "axios";
import { IPosition } from "./IPosition";
import mqtt, { MqttClient } from "mqtt";
export interface ISend {
    data: IPosition;
    protocol : MqttClient;
    topic_data: string;
    topic_response:string;
}