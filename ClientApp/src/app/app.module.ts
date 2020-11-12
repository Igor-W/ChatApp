import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {DataHandlerService} from './services/data-handler.service';
import {ShareService} from './services/share.service';
import { AppComponent } from './app.component';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {MenuComponent} from './views/menu/menu.component';
import {ContactsComponent} from './views/contacts/contacts.component';
import {ChatComponent} from './views/chat/chat.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {Routes, RouterModule} from '@angular/router';
import { AboutComponent } from './views/about/about.component';
import { TeamsComponent } from './views/teams/teams.component';
import { AdminPageComponent } from './views/admin-page/admin-page.component';
import { LoginPageComponent } from './views/login-page/login-page.component';
import { ChatApplicationComponent } from './views/chat-application/chat-application.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ConfirmDialogComponent } from './views/confirm-dialog/confirm-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule} from '@angular/material/button';
import {AuthGuard} from './guards/auth.guard';
import {AdminGuard} from './guards/admin.guard';

import { HttpInterceptorService } from './services/http-interceptor.service';
import { ErrorInterceptorService } from './services/error-interceptor.service';

import { ChatService } from './services/chat.service';




const appRoutes: Routes = [
  {path: '', component: LoginPageComponent},
  { path: 'api/user-panel', component: ChatApplicationComponent, canActivate: [AuthGuard]},
  { path: 'api/admin-panel', component: AdminPageComponent, canActivate: [AdminGuard]},
  {path: '**', redirectTo: '/'}
];
@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    ContactsComponent,
    ChatComponent,
    AboutComponent,
    TeamsComponent,
    AdminPageComponent,
    LoginPageComponent,
    ChatApplicationComponent,
    ConfirmDialogComponent,

  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    MatDialogModule,
    MatButtonModule
  ],
  providers: [
    ChatService,
    DataHandlerService,
    ShareService,
    { provide: MatDialogRef, useValue: {} },
    { provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptorService, multi: true },
    { provide: MAT_DIALOG_DATA, useValue: [] }
  ],
  entryComponents: [
    ConfirmDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
