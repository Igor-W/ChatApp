import {Component, OnInit} from '@angular/core';
import {ShareService} from '../../services/share.service';
import {User} from '../../Model/User';
import {Message} from '../../Model/Message';
import { DataHandlerService } from '../../services/data-handler.service';
import { ChatService } from '../../services/chat.service';
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  mainUser: User = null;
  messages: Message[] = null;
  currentMessage: Message = new Message();
  logedInUserId: string;

  constructor(private share: ShareService, private dataHandler: DataHandlerService, private chatService: ChatService) {
    this.share.userClick.subscribe(usr => this.mainUser = usr);
    this.share.logedInUserIdEventEmitter.subscribe(usr => this.logedInUserId = usr);
    this.share.getMessagesOnClick.subscribe(msg => { this.messages = msg; this.filterMessages() });
    this.share.recievedMessageEmitter.subscribe(msg => {
      if (this.messages.includes(msg) == false)
        this.messages.push(msg);
      this.filterMessages();
    });

    //this.share.getMessagesOnClick.subscribe(msgs => this.messages = msgs.sort((a, b) => {
    //  return b.createdTime - a.createdTime;
    //}));
    //if (this.messages != null) {
    //  this.messages.sort((a, b) => {
    //    return b.createdTime.getDate() - a.createdTime.getDate();
    //  });
    //} 
  }


  ngOnInit(): void {
  }

  onButtonClick() {
   
  
    this.currentMessage.senderId = this.logedInUserId;
    this.currentMessage.recieverId = this.mainUser.id;
    console.log("main user: " + this.mainUser.id)
    this.currentMessage.status = 'SENT';
    this.currentMessage.createdAt = new Date();
    this.chatService.sendMessage(this.mainUser.id.toString(),this.currentMessage);
    this.currentMessage = new Message();
  }
  filterMessages(): void {
    this.messages.sort((a, b) => (a.createdAt > b.createdAt) ? -1 : 1);
  }
}
