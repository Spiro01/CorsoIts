## Progetto protocolli Iot

   - Client (Dispositivi edge) - **Locatello Jacopo Mattia**

   - Server (Cloud) - **Spironelli Riccardo**
### Implementazione Mqtt:
   - Broker = Mosquitto
   - Connessione tra client e broker utilizzando ngrok
   - Topic per la posizione del drone= DroneRental/id_drone/status
   - Topic per la ricezione dei comandi da parte del drone= DroneRental/id_drone/command
   - Topic per la conferma della ricezione comando=DroneRental/id_drone/feedback
   - Tempo che intercorre tra l'invio di uno stato e l'altro= 1sec
   - Tempo per la ricezione dei comandi= asap.
   - Tempo per la risposta=asap.
   - I dati viaggiano in chiaro.
   
