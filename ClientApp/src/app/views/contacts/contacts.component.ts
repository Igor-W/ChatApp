import { Component, OnInit } from '@angular/core';
import { DataHandlerService } from '../../services/data-handler.service';
import { User } from '../../Model/User';
import { ShareService } from '../../services/share.service';




@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.css']
})
export class ContactsComponent implements OnInit {
  users: User[];
  selectedUser: User;
  messages: any = null;
  searchName = '';
  authorizedUserId: string;
  isMessagesRecieved: boolean = false;
  constructor(private dataHandler: DataHandlerService, private share: ShareService) {

  }


  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.dataHandler.fillUsersContacts()
      .subscribe((data: User[]) => this.users = data)
  }

  setUserDetails() {
    if (localStorage.getItem('authToken')) {
      const decodeUserDetails = JSON.parse(window.atob(localStorage.getItem('authToken').split('.')[1]));
      this.authorizedUserId = decodeUserDetails.nameid;
    }
  }

  clickedContact(user: User): void {
    this.setUserDetails();
    this.selectedUser = user;

    this.dataHandler.getMessagesByUser(this.authorizedUserId, user.id.toString())
      .subscribe(data => this.messages = data,
        error => console.log("Error", error),
        () => {
          this.share.logedInUserId = this.authorizedUserId;
          this.share.clickedMessages = this.messages;
          this.share.clickedUser = user;
          this.share.clickOnUser();
        }
      );
  }
}
