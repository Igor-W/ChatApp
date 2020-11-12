/// <reference path="../../model/roles.ts" />
import { Component, OnInit, Inject } from '@angular/core';
import { DataHandlerService } from '../../services/data-handler.service';
import { User } from '../../Model/User';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from "@angular/material/dialog";
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPageComponent implements OnInit {

  allUsers: User[];
  user: User = new User();
  genderValue: string;
  tableMode = true;
  currentUserId: number;

  constructor(
    private dialogRef: MatDialogRef<AdminPageComponent>,
    private logOutDialogRef: MatDialogRef<AdminPageComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dataHandler: DataHandlerService,
    private dialog: MatDialog,
    private logOutdialog: MatDialog,
    private authService: AuthService
  ) {
  }

  ngOnInit() {
    this.loadAllUsers();
    this.setCurrentUserId();
  }

  loadAllUsers() {
    this.dataHandler.getAllUsers()
      .subscribe((data: User[]) => this.allUsers = data);
  }

  setCurrentUserId() {
    if (localStorage.getItem('authToken')) {
      let userDetails = new User();
      const decodeUserDetails = JSON.parse(window.atob(localStorage.getItem('authToken').split('.')[1]));
      this.currentUserId = decodeUserDetails.nameid;
    }
  }

  editUser(user: User) {
    this.user = user;
  }

  save() {
    if (this.user.id == null) {
      this.genderValue = (<HTMLSelectElement>document.getElementById('inputGroupSelect01')).value;
      if (this.genderValue === 'female') {
        this.user.gender = true;
      } else {
        this.user.gender = false;
      }
      this.dataHandler.createUser(this.user)
        .subscribe((data: User) => this.allUsers.push(data));
      console.log(this.user);
    } else {
      this.dataHandler.updateUser(this.user)
        .subscribe(data => this.loadAllUsers());
    }
    this.cancel();
  }

  cancel() {
    this.user = new User();
    this.tableMode = true;
  }

  delete(user: User) {
    let userToDeleteId = user.id;
    this.dataHandler.deleteUser(userToDeleteId)
      .subscribe(data => this.loadAllUsers());
    if (userToDeleteId == this.currentUserId)
      this.logOut();
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }

  openConfirmDialog(user: User) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      height: '200px',
      data: {
        dialogTitle: 'Confirm your action',
        message: `Would you like to delete user: "${user.firstName} ${user.lastName}"?`
      },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(user);
        this.dialogRef.close;
      }
    });
  }

  openLogOutDialog() {
    const logOutDialogRef = this.logOutdialog.open(ConfirmDialogComponent, {
      width: '350px',
      height: '200px',
      data: {
        dialogTitle: 'Confirm your action',
        message: `Would you like to logout"?`
      },
      autoFocus: false
    });
    logOutDialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.logOut();
        this.logOutDialogRef.close;
      }
    });
  }

  logOut() {
    this.authService.logout();
  }

}
