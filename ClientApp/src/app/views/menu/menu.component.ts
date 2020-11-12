import { Component, OnInit, Input, Output, EventEmitter, Inject, OnChanges } from '@angular/core';
import { DataHandlerService } from '../../services/data-handler.service';
import { User } from '../../Model/User';
import { Tab } from '../../Model/Tab';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from "@angular/material/dialog";
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { ShareService } from '../../services/share.service';
import { Router } from '@angular/router';




@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  authorizedUser: User;
  @Input() selectedTab: Tab;
  tabs: Tab[];
  @Output() onClick = new EventEmitter();
  firsrtTab: Tab;

  constructor(
    private dataHandler: DataHandlerService,
    private dialogRef: MatDialogRef<MenuComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private dialog: MatDialog,
    private router: Router
  )
  {
    this.firsrtTab = new Tab('fas fa-comment-alt', 'chat');
    this.tabs =
      [
        new Tab('fas fa-user ', 'about me'),
        new Tab('fas fa-comment-alt', 'chat'),
        new Tab('fas fa-users', 'my teams')
      ];
  }

  ngOnInit(): void {
    this.dataHandler.getAuthorizedUser().subscribe(data =>
      this.authorizedUser = data);
  }



  clickOnTab(tab: Tab): void {
    this.selectedTab = tab;
    this.onClick.emit(this.selectedTab);
  }

  openConfirmDialog(user: User) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      height: '200px',
      data: {
        dialogTitle: 'Confirm your action',
        message: 'Would you like to log out?'
      },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.router.navigate(['/']);
        this.dialogRef.close;
      }
    });
  }

}
