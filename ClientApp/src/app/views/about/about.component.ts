import { Component, OnInit } from '@angular/core';
import { User } from '../../Model/User';
import { DataHandlerService } from '../../services/data-handler.service';
import { ShareService } from '../../services/share.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  authorizedUser: User = new User();
  editMode: boolean = true;


  constructor(private dataHandler: DataHandlerService,
    private share: ShareService) {
  }

  ngOnInit() {
    this.dataHandler.getAuthorizedUser().subscribe(data => this.authorizedUser = data);
  }

  editCurentUser() {
    this.editMode = false;
  }

  save() {
    this.dataHandler.updateUserByItself(this.authorizedUser)
      .subscribe(data => this.authorizedUser === data);
    window.location.reload();
    this.cancel();
  }

  cancel() {
    this.editMode = true;
  }

}
