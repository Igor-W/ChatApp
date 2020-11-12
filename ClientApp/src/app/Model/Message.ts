export class Message {
  senderId?: string;
  recieverId?: number;
  message?: string;
  status?: string;
  createdAt?: Date;

  /*
  constructor(senderId: number, receiverId: number, message: string, messageStatus: boolean, createdTime: Date) {
    //this.senderId = senderId;
    //this.receiverId = receiverId;
    //this.message = message;
    //this.messageStatus = messageStatus;
    //this.createdTime = createdTime;
  }
  */


  constructor() {
    this.message = '';

  }
}
