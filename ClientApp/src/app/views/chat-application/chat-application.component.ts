import {Component, OnInit} from '@angular/core';
import {Tab} from '../../Model/Tab';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat-application',
  templateUrl: './chat-application.component.html',
  styleUrls: ['./chat-application.component.css']
})
export class ChatApplicationComponent implements OnInit {
  activeTab: Tab;

  constructor() {
  }

  upCase(tab: Tab): void {
    this.activeTab = tab;
  }

  ngOnInit(): void {
    this.activeTab = new Tab('fas fa-user ', 'chat');
  }

}


