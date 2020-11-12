import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { User } from '../Model/User';
import { Message } from '../Model/Message';


@Injectable({
  providedIn: 'root'
})
export class ShareService {

  constructor() { }


  clickedUser: User;
  clickedMessages: Message[];
  recievedMessage: Message;
  logedInUserId: string;



  logedInUserIdEventEmitter: EventEmitter<string> = new EventEmitter();
  userClick: EventEmitter<User> = new EventEmitter();
  getMessagesOnClick: EventEmitter<Message[]> = new EventEmitter();
  recievedMessageEmitter: EventEmitter<Message> = new EventEmitter();

  public clickOnUser() {
    this.logedInUserIdEventEmitter.emit(this.logedInUserId);
    this.userClick.emit(this.clickedUser);
    this.getMessagesOnClick.emit(this.clickedMessages);
  }

  public getRecievedMessage() {
    this.recievedMessageEmitter.emit(this.recievedMessage);
  }

}
