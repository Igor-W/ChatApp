import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from '../Model/Message';
import { ShareService } from './share.service';


@Injectable()
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connectionEstablished = new EventEmitter<Boolean>();

  private connectionIsEstablished = false;
  private _hubConnection: HubConnection;

  constructor(private share : ShareService) {
    
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendMessage(currentUserId: string, message: Message) {
    this._hubConnection.invoke('SendMessage', currentUserId, message);
  }

  ReceiveMessage(message: string) {
    console.log(message);
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44344/MessageHub')
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      })
      .catch(err => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(function () { this.startConnection(); }, 5000);
      });
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('ReceiveMessage', (data: Message) => {
      console.log("--------MESSAGE RECIEVED----------");
      console.log(data);
      this.share.recievedMessage = data;
      this.share.getRecievedMessage();

      console.log("----------------------------------");


    });
  }
}  
