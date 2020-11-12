import { Injectable } from '@angular/core';
import { User } from '../Model/User';
import { Message } from '../Model/Message';
import { HttpClient } from '@angular/common/http';
import { ShareService } from './share.service';


@Injectable({
  providedIn: 'root'
})
export class DataHandlerService {

  authorizedUser: User;
  mainUser: User;

  adminUrl: string = 'api/admin-panel';
  userUrl: string = 'api/user-panel';
  messages: Message[];

  constructor(
    private http: HttpClient,
    private share: ShareService
  )
  {
    this.share.userClick.subscribe(usr => this.mainUser = usr);
  }

  getAllUsers() {
    return this.http.get(this.adminUrl + '/all-users');
  }

  deleteUser(id: number) {
    return this.http.delete(this.adminUrl + '/delete-user' + '/' + id);
  }

  createUser(user: User) {
    return this.http.post(this.adminUrl + '/create-user', user);
  }

  updateUser(user: User) {
    return this.http.put(this.adminUrl + '/update-user' + '/' + user.id, user);
  }

  updateUserByItself(user: User) {
    return this.http.put(this.userUrl + '/update-user' + '/' + user.id, user);
  }

  fillUsersContacts() {
    return this.http.get(this.userUrl + '/all-users');
  }

  getMessagesByUser(autId: string, mainId: string) {
    return this.http.get('/api/conversation/' + autId + '&&' + mainId);
  }

  getAuthorizedUser() {
    return this.http.get(this.userUrl + '/get-current-user');
  }

}
